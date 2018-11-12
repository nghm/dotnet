﻿namespace Hypermedia.AspNetCore.Siren.Util
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Internal;

    internal static class ActionDescriptorMethods
    {
        public static string GetHttpMethod(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor
                .GetHttpMethods()
                .Single();
        }

        public static IEnumerable<string> GetHttpMethods(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor
                .ActionConstraints
                .OfType<HttpMethodActionConstraint>()
                .SelectMany(constraint => constraint.HttpMethods);
        }

        public static IEnumerable<object> PickBodyArguments(this ActionDescriptor descriptor, object[] arguments)
        {
            return descriptor.PickArguments(arguments, "Body");
        }

        public static object PickBodyArgument(this ActionDescriptor descriptor, object[] arguments)
        {
            return descriptor
                .PickBodyArguments(arguments)
                .SingleOrDefault();
        }
        
        private static IEnumerable<object> PickArguments(this ActionDescriptor descriptor, object[] arguments, string id)
        {
            var parameters = descriptor.Parameters;
            var index = -1;

            foreach (var parameter in parameters)
            {
                index++;

                if (parameter.BindingInfo.BindingSource.Id != id)
                {
                    continue;
                }

                yield return arguments[index];
            }
        }

    }
}
