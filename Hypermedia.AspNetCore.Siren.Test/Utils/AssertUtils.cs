namespace Hypermedia.AspNetCore.Siren.Test.Utils
{
    using System;
    using Xunit;

    internal static class AssertUtils
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
