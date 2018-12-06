namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Builders.Abstractions;

    internal interface IFieldFactory
    {
        IField MakeField(FieldDescriptor fieldDescriptor);
    }
}
