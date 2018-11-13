namespace Hypermedia.AspNetCore.ApiExport.AssemblyAnalyzer
{
    internal interface IAssemblyAnalyzer
    {
        IAssemblyAnalysisResult Analyze(string path);
    }
}