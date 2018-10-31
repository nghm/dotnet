namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;

    internal interface IControllerTypeChecker
    {
        bool IsController(Type controllerType);
    }
}