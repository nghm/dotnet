using System;
using Hypermedia.AspNetCore.Siren.Endpoints;
using Hypermedia.AspNetCore.Siren.Util;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldsFactory : IFieldsFactory
    {
        private readonly IFieldFactory _fieldFactory;

        public FieldsFactory(IFieldFactory fieldFactory)
        {
            this._fieldFactory = 
                fieldFactory ??
                throw new ArgumentNullException(nameof(fieldFactory));
        }

        public IEnumerable<IField> MakeFields(ActionArgument bodyArgument)
        {
            if (bodyArgument == null)
            {
                throw new ArgumentNullException(nameof(bodyArgument));
            }

            var argumentFields = bodyArgument.FieldDescriptors;

            foreach (var field in argumentFields)
            {
                yield return _fieldFactory.MakeField(field);
            }
        }

        
    }
}