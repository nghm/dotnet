namespace Hypermedia.AspNetCore.Tests.Common
{
    using System;
    using Xunit;

    public static class AssertUtils
    {
        public static void NoExceptions(Action act)
        {
            try
            {
                act();
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Exception was thrown when none was expected. Exception ({ex.GetType().Name}) message: {ex.Message}");
            }
        }
    }
}
