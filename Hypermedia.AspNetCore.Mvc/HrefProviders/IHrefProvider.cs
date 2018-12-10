namespace Hypermedia.AspNetCore.Mvc.HrefProviders
{
    internal interface IHrefProvider
    {
        string Get(object[] arguments);
    }
}