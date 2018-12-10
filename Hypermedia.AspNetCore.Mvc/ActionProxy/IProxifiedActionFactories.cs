namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using System.Reflection;

    internal interface IProxifiedActionFactories
    {
        IProxifiedActionFactory Get(MethodInfo method);
    }
}