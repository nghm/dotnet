namespace Hypermedia.AspNetCore.ApiExport
{
    public interface IAssemblyAnalyzer
    {
        IAssemblyAnalysisResult Analyze(string path);
    }
}