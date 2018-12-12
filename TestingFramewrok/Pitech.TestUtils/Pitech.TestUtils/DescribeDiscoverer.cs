using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Pitech.TestUtils
{
    using System;

    public class DiscovererUtil
    {
        internal const string AssemblyName = nameof(Pitech) + "." + nameof(Pitech.TestUtils);

    }

    public class DescribeDiscoverer : ITraitDiscoverer
    {
        internal const string DiscovererTypeName = DiscovererUtil.AssemblyName + "." + nameof(DescribeDiscoverer);

        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var systemUnderTest = traitAttribute.GetNamedArgument<Type>("SystemUnderTest");

            yield return new KeyValuePair<string, string>("Category", "Unit Test");

            if (systemUnderTest != null)
            {
                yield return new KeyValuePair<string, string>("System Under Test", systemUnderTest.Name);
            }
        }
    }
}