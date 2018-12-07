namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using System.Reflection;

    internal interface IApiActionDescriptors
    {
        IApiActionDescriptor Get(MethodInfo action);
    }
}