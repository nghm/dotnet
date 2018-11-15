namespace Hypermedia.AspNetCore.ApiExport
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Options;

    internal class Exporter : IExporter
    {
        private readonly IOptions<ApplicationOptions> _options;
        private readonly ITypeFactory _typeFactory;

        public Exporter(IOptions<ApplicationOptions> options, ITypeFactory typeFactory)
        {
            this._options = options;
            this._typeFactory = typeFactory;
        }

        public void Export(ExportDefinition exportDefinition)
        {
            var response =
                "using Hypermedia.AspNetCore.Siren;\n" +
                "using Microsoft.AspNetCore.Mvc;\n" +
                "using Microsoft.Extensions.DependencyInjection;\n" +
                "\n" +
                "namespace Hypermedia.External {\n" +
                "   public static class " + this._options.Value.ExportAssemblyName + "Extensions\n" +
                "   {\n" +
                "       public static IMvcBuilder Add" + this._options.Value.ExportAssemblyName +
                "(this IMvcBuilder builder)\n" +
                "       {\n" +
                "          builder.AddHypermediaExternalModule(GetType().Assembly)\n" +
                "          \n" +
                "          return builder;\n" +
                "       }\n" +
                "   }\n" +
                "}\n" +
                "\n" +
                "namespace Hypermedia.External." + this._options.Value.ExportAssemblyName + "\n" +
                "{\n";
            foreach (var controller in exportDefinition.Controllers)
            {
                response +=
                        "       [Microsoft.AspNetCore.Mvc.ApiControllerAttribute()]\n" +
                        "       public class " + controller.Name + "\n" +
                        "       {\n";

                    foreach (var action in controller.Actions)
                    {
                        response +=
                        "           [Microsoft.AspNetCore.Mvc.RouteAttribute(\"" + action.Route + "\")]\n" +
                        "           [" + action.Methods.Join(", ") + "]\n" +
                        "           public virtual void " + action.Name + "(" + action.Parameters.Select(p => p.ToString()).Join(", ") + ")\n" +
                        "           {\n" +
                        "           }\n";
                    }
                response += 
                        "       }\n";
            }

            response += 
                GetExportedTypes() +
                        "}";

            var st = CSharpSyntaxTree.ParseText(response, encoding: System.Text.Encoding.UTF8);
            
            var fileName = this._options.Value.ExportAssemblyName + ".dll";

            Compilation compilation = CSharpCompilation
                .Create(fileName)
                .WithOptions(
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, true, allowUnsafe: true)
                )
                .AddReferences()
                .AddSyntaxTrees(st);

            var types = new[]
            {
                typeof(object),
                typeof(ApiControllerAttribute),
                typeof(IActionResult)
            };

            foreach (var type in types)
            {
                var location = type.GetTypeInfo().Assembly.Location;

                var reference = MetadataReference.CreateFromFile(location);

                compilation = compilation.AddReferences(reference);
            }

            var assemblies = (string) AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES");

            foreach (var assembly in assemblies.Split(";"))
            {
                var reference = MetadataReference.CreateFromFile(assembly);

                compilation = compilation.AddReferences(reference);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var compilationResult = compilation.Emit(path);
        }


        private string GetExportedTypes()
        {
            var types = this._typeFactory.GetAllTypes().ToArray();

            return
                "   namespace Models\n" +
                "   {\n" +
                        this._typeFactory.GetReferences() + "\n" +
                        types
                            .Select(t => t.ToString())
                            .Join("\n") + "\n" +
                "   }\n";
        }

        private string Sanitize(string str)
        {
            return str.Replace(".", "");
        }
    }
}