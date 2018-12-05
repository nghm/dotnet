using System.Collections.Generic;
using System.Text;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal interface IFieldFactory
    {
        IField MakeField(FieldDescriptor fieldDescriptor);
    }
}
