namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Actions.Fields;
    using Builders.Abstractions;
    using System.Collections.Generic;

    internal interface IFieldsFactory
    {
        IEnumerable<IField> MakeFields(ActionArgument bodyArgument);
    }
}