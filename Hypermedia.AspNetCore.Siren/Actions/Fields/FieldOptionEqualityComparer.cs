using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldOptionEqualityComparer : IEqualityComparer<FieldOption>
    {
        public bool Equals(FieldOption option1, FieldOption option2)
        {
            return option1.Name == option2.Name
                   && option1.Value == option2.Value;
        }

        public int GetHashCode(FieldOption option)
        {
            return option.GetHashCode();
        }
    }
}