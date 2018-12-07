namespace Hypermedia.AspNetCore.Mvc
{
    using ApiExploration;

    internal interface IHrefFactories
    {
        IHrefFactory Get(IApiActionDescriptor descriptor);
    }
}