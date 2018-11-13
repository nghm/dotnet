namespace Hypermedia.AspNetCore.ApiExport.AssemblyAnalyzer
{
    using System;

    internal interface IActionDescriptorContextFactory
    {
        ActionDescriptorContext Make(Type controllerType);
    }
}