namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;

    internal interface IEnumOptionsExtractor
    {
        bool TryGetEnumOptions(Type propertyType, out FieldOption[] options);
    }
}