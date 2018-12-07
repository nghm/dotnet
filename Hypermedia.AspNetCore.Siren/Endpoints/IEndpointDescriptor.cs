﻿namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Actions.Fields;
    using Microsoft.AspNetCore.Authorization;

    internal interface IEndpointDescriptor
    {
        AuthorizationPolicy[] Policies { get; }
        string Host { get; }
        string Protocol { get; }
        string Method { get; }
        object Body { get; }
        string Template { get; }
        ActionArgument BodyArgument { get; }
        ArgumentCollection ArgumentsCollection { get; }
    }
}