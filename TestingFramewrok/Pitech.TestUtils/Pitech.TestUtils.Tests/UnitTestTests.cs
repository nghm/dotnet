namespace Pitech.TestUtils.Tests
{
    using System;
    using Xunit;

    [SystemUnderTest(typeof(UnitTest))]
    public class UnitTestTests
    {
        [Dependencies(OneAtATime = OneAtATime.Null)]
        [Constructor, Should("throw ArgumentNullException when constructor [Sut]{dependencies} is called")]
        public void ArgumentNullException(
            object[] dependencies,
            ISutFactory<UnitTest> sutFactory
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                sutFactory.Make(dependencies);
            });
        }
    }

    public enum OneAtATime
    {
    }

    public class UnitTest
    {
        public UnitTest(string name, string name2)
        {

        }
    }
}
