namespace Hypermedia.AspNetCore.ApiExport
{
    internal interface IPackageCompiler
    {
        string Compile(IAssemblyAnalysisResult result);
    }
}