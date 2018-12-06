using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldOptionEqualityComparer : IEqualityComparer<FieldOption>
    {
        public bool Equals(FieldOption option1, FieldOption option2)
        {
            if (option1 == null || option2 == null)
            {
                return object.Equals(option1, option2);
            }

            return option1.Name == option2.Name
                   && object.Equals(option1.Value,option2.Value);
        }

        public int GetHashCode(FieldOption option) => option.GetHashCode();
    }
}