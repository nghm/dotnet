namespace Hypermedia.AspNetCore.Siren.Test
{
    using Endpoints;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class Collectable
    {
        public void CollectMe(int a, int b, int c = 0)
        {
        }
    }

    public class ProxyCollectTests
    {
        protected internal MethodInfo CollectMeMethodInfo = typeof(Collectable)
            .GetMethods()
            .First(m => m.Name == nameof(Collectable.CollectMe));

        [Theory]
        [AutoMockData]
        private void TestPerformanceOld(MethodMethodCallPlucker sut)
        {
            sut.PluckMethodCall<Collectable>(e => e.CollectMe(1, 2, 3 + 2), out var callCollected);

            Assert.Equal(callCollected.Method, this.CollectMeMethodInfo);
            Assert.Equal(callCollected.Arguments.OfType<int>(), new[] { 1, 2, 5 });
        }
    }
}
