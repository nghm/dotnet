namespace Hypermedia.AspNetCore.Mvc
{
    internal interface IHrefFactories
    {
        IHrefFactory Get(IApiActionDescriptor descriptor);
    }
}