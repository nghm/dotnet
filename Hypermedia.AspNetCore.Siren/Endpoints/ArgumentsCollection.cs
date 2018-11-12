namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal class ArgumentsCollection : Dictionary<ControllerParameterDescriptor, object>
    {
        public ArgumentsCollection(ControllerParameterDescriptor[] parameters, object[] endpointDescriptors)
            : base(parameters.ToDictionary(
                    parameter => parameter,
                    parameter => endpointDescriptors[parameter.ParameterInfo.Position]
                ))
        {
        }
    }
}