namespace Pitech.TestUtils
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xunit.Sdk;

    public class Dependencies : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return testMethod
                .DeclaringType
                .GetCustomAttribute<SystemUnderTestAttribute>()
                .SystemUnderTest
                .GetConstructors()
                .FirstOrDefault()
                ?.GetParameters()
                .Select(parameter => new[] { parameter });
        }

        public object OneAtATime
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}