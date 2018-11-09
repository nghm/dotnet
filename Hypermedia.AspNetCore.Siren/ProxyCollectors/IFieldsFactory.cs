namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Collections.Generic;
    using Actions.Fields;
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal interface IFieldsFactory
    {
        IEnumerable<IField> MakeFields(KeyValuePair<ControllerParameterDescriptor, object> bodyArgument);
    }
}