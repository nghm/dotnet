namespace Pitech.TestUtils
{
    using System;
    using Xunit;
    using Xunit.Sdk;

    [XunitTestCaseDiscoverer("Pitech.TestUtils.ShouldDiscoverer", "Pitech.TestUtils")]
    [AttributeUsage(AttributeTargets.Method)]
    public class ShouldAttribute : FactAttribute
    {
    }
}