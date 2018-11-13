namespace Hypermedia.AspNetCore.ApiExport
{
    public interface IPackageCompiler
    {
        string Compile(IAssemblyAnalysisResult result);
    }
}