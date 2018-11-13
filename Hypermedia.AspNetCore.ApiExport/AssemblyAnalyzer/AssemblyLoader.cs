namespace Hypermedia.AspNetCore.ApiExport.AssemblyAnalyzer
{
    using System.Reflection;

    internal class AssemblyLoader : IAssemblyLoader
    {
        public Assembly Load(string path)
        {
            return Assembly.LoadFile(path);
        }
    }
}