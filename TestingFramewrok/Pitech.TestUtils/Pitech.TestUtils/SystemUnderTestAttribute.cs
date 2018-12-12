using System;
using Xunit.Sdk;

namespace Pitech.TestUtils
{
    [TraitDiscoverer(DescribeDiscoverer.DiscovererTypeName, DiscovererUtil.AssemblyName)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SystemUnderTestAttribute : Attribute, ITraitAttribute
    {
        public SystemUnderTestAttribute(Type systemUnderTest)
        {
            this.SystemUnderTest = systemUnderTest;
        }

        public Type SystemUnderTest { get; private set; }

    }
}