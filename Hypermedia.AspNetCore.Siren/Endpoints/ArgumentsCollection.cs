using Hypermedia.AspNetCore.Siren.Actions.Fields;
using System.Reflection;

namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Linq;

    internal class ArgumentCollection : List<ActionArgument>
    {
        public ArgumentCollection(ControllerParameterDescriptor[] parameters, object[] values)
            : base(parameters.Select(parameter =>
            {
                return new ActionArgument(
                    parameter.Name,
                    values[parameter.ParameterInfo.Position],
                    parameter.ParameterInfo.DefaultValue,
                    parameter.BindingInfo.BindingSource,
                    parameter.ParameterType.GetProperties().Where(IsField)
                        .Select(p => new FieldDescriptor(
                            p.Name,
                            p.GetValue(values[parameter.ParameterInfo.Position]),
                            p.PropertyType,
                            p.PropertyType.GetCustomAttributes(true)))
                        .ToArray());
            }))
        {
        }

        private static bool IsField(PropertyInfo p)
        {
            return p.SetMethod != null && p.SetMethod.IsPublic && !p.SetMethod.IsAbstract;
        }
    }
}