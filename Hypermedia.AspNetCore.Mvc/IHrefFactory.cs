namespace Hypermedia.AspNetCore.Mvc
{
    internal interface IHrefFactory
    {
        string Make(object[] arguments);
    }
}