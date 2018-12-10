namespace Hypermedia.AspNetCore.Mvc
{
    using ApiExploration;

    internal interface IHrefProviders
    {
        IHrefProvider Get(IApiActionDescriptor descriptor);
    }
}