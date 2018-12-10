namespace Hypermedia.AspNetCore.Mvc
{
    internal interface IHrefProvider
    {
        string Get(object[] arguments);
    }
}