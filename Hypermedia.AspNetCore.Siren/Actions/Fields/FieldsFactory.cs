using Hypermedia.AspNetCore.Siren.Endpoints;
using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Builders.Abstractions;
    using System.Linq;

    internal class FieldsFactory : IFieldsFactory
    {
        private readonly IFieldFactory _fieldFactory;

        public FieldsFactory(IFieldFactory fieldFactory)
        {
            this._fieldFactory =
                fieldFactory ??
                throw new ArgumentNullException(nameof(fieldFactory));
        }

        public IFields MakeFields(ActionArgument bodyArgument)
        {
            if (bodyArgument == null)
            {
                throw new ArgumentNullException(nameof(bodyArgument));
            }

            var argumentFields = bodyArgument.FieldDescriptors;

            return new Fields(argumentFields.Select(argument => this._fieldFactory.MakeField(argument)));
        }
    }
}