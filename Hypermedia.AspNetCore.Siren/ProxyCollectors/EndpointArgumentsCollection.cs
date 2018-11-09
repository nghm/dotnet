namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal class EndpointArgumentsCollection : Dictionary<ControllerParameterDescriptor, object>
    {
        public EndpointArgumentsCollection(ControllerParameterDescriptor[] parameters, object[] endpointDescriptors)
            : base(parameters.ToDictionary(
                    parameter => parameter,
                    parameter => endpointDescriptors[parameter.ParameterInfo.Position]
                ))
        {
        }
    }
}