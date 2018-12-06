namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Actions.Fields;
    using Builders.Abstractions;

    internal interface IFieldsFactory
    {
        IFields MakeFields(ActionArgument bodyArgument);
    }
}