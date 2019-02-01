namespace Pitech.TestUtils.Tests
{
    //[SystemUnderTest(typeof(UnitTest))]
    public class UnitTestTests
    {
        //[Dependencies]
        //[Constructor, Should("throw ArgumentNullException when constructor [Sut]{dependencies} is called")]
        public void ArgumentNullException(
            object[] dependencies
        )
        {
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    sutFactory.Make(dependencies);
            //});
        }
    }

    public enum OneAtATime
    {
    }

}
