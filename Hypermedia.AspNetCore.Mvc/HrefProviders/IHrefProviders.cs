namespace Hypermedia.AspNetCore.Mvc.HrefProviders
{
    using ApiExploration;

    internal interface IHrefProviders
    {
        IHrefProvider Get(IApiActionDescriptor descriptor);
    }
}