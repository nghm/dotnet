namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;
    using AssemblyAnalyzer;

    internal interface IAssemblyAnalysisResult
    {
        IEnumerable<ActionDescriptorContext> ActionDescriptorContexts { get; }
    }
}