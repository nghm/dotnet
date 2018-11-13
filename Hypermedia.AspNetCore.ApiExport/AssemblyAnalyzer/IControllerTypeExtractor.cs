namespace Hypermedia.AspNetCore.ApiExport.AssemblyAnalyzer
{
    using System;
    using System.Reflection;

    internal interface IControllerTypeExtractor
    {
        Type[] Extract(Assembly assembly);
    }
}