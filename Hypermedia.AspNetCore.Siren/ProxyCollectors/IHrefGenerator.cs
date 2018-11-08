namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal interface IHrefGenerator
    {
        string ComputeHref(EndpointDescriptor endpointDescriptor);
    }
}