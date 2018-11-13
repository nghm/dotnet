namespace Hypermedia.AspNetCore.ApiExport.AssemblyAnalyzer
{
    using System.Reflection;

    internal interface IAssemblyLoader
    {
        Assembly Load(string path);
    }
}