namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;

    [Obsolete]
    internal interface IHrefFactory
    {
        string MakeHref(IEndpointDescriptor endpointDescriptor);
    }
}