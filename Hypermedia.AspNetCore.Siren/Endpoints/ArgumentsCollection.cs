using Hypermedia.AspNetCore.Siren.Actions.Fields;

namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Linq;

    internal class ArgumentCollection : List<ActionArgument>
    {
        public ArgumentCollection(ControllerParameterDescriptor[] parameters, object[] endpointDescriptors)
            : base(parameters.Select(p => new ActionArgument(p, endpointDescriptors[p.ParameterInfo.Position])))
        {
        }
    }
}