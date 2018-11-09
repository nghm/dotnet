namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal interface IHrefFactory
    {
        string MakeHref(EndpointDescriptor endpointDescriptor);
    }
}