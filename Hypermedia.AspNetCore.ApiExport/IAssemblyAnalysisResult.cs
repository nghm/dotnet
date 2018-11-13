namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;

    public interface IAssemblyAnalysisResult
    {
        IEnumerable<IEndpoint> Endpoints { get; }
    }
}