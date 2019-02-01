namespace Pitech.TestUtils
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    internal class ShouldDiscoverer : TheoryDiscoverer
    {
        public ShouldDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
        {
        }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForDataRow(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo theoryAttribute,
            object[] dataRow)
        {
            return new[]
            {
                new PitechTestCase(
                    this.DiagnosticMessageSink,
                    discoveryOptions.MethodDisplayOrDefault(),
                    discoveryOptions.MethodDisplayOptionsOrDefault(),
                    testMethod,
                    dataRow
                )
            };
        }
    }

    internal class PitechTestCase : XunitTestCase, IXunitTestCase
    {
        public PitechTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay methodDisplayOrDefault, TestMethodDisplayOptions methodDisplayOptionsOrDefault, ITestMethod testMethod, object[] dataRow)
            : base(diagnosticMessageSink, methodDisplayOrDefault, methodDisplayOptionsOrDefault, testMethod, dataRow)
        {

        }

        // ReSharper disable once UnusedMember.Global
#pragma warning disable 618
        public PitechTestCase()
#pragma warning restore 618
        {

        }

        protected override string GetDisplayName(IAttributeInfo factAttribute, string displayName)
        {
            var parameters = this.TestMethod.Method.GetParameters().ToList();

            displayName = "should " + this.TestMethod.Method.Name;

            return Regex.Replace(displayName,
                "\\{ *([a-zA-Z]+) *\\}",
                match =>
                {
                    var parameterName = match.Groups
                        .Skip(1)
                        .First()
                        .ToString();

                    var parameterIndex = parameters.FindIndex(p => parameterName == p.Name);

                    return this.TestMethodArguments[parameterIndex].ToString();
                });
        }
    }
}
