namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    internal interface IProxifiedActionFactory
    {
        IProxifiedAction Make(object[] arguments);
    }
}