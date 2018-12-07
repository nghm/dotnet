using System.Threading.Tasks;

namespace Hypermedia.AspNetCore.Tests.Common
{
    using System;
    using Xunit;

    public static class AssertUtils
    {
        public static void NoExceptions<T>(Func<T> act)
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

        public static void IgnoreException<T>(Func<object> act) where T : Exception
        {
            try
            {
                act();
            }
            catch (T)
            {

            }
        }
        public static async Task IgnoreExceptionAsync<T>(Func<Task> act) where T : Exception
        {
            try
            {
                await act();
            }
            catch (T)
            {

            }
        }
    }
}
