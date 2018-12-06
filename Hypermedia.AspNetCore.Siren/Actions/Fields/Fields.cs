namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Builders.Abstractions;
    using System.Collections.Generic;

    internal class Fields : List<IField>, IFields
    {
        public Fields(IEnumerable<IField> fields)
            : base(fields)
        {
        }
    }
}