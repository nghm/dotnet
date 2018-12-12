namespace Pitech.TestUtils
{
    using System;
    using Xunit;
    using Xunit.Sdk;

    [XunitTestCaseDiscoverer("Pitech.TestUtils.ShouldDiscoverer", "Pitech.TestUtils")]
    [AttributeUsage(AttributeTargets.Method)]
    public class ShouldAttribute : FactAttribute
    {
        private readonly string _should;

        public ShouldAttribute(string should)
        {
            this._should = should;
        }

        public override string DisplayName => $"should {this._should}";
    }
}