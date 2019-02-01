namespace Hypermedia.AspNetCore.Tests.Common
{
    using AutoFixture.Xunit2;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AutoServiceDataAttribute : AutoDataAttribute
    {
        private readonly Type[] _services;

        public AutoServiceDataAttribute(params Type[] services)
        {
            this._services = services;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var result = base.GetData(testMethod);

            return result;
        }
    }
}
