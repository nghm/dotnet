namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.Collections.Generic;

    internal interface ITypeMetaProvider
    {
        IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldDescriptor fieldDescriptor);
    }
}
