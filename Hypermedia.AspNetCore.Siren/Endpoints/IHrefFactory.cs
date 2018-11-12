namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    internal interface IHrefFactory
    {
        string MakeHref(EndpointDescriptor endpointDescriptor);
    }
}