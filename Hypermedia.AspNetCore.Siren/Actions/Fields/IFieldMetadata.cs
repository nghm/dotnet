using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    public interface IFieldMetadata
    {
        IEnumerable<KeyValuePair<string, object>> GetMetadata();
    }
}