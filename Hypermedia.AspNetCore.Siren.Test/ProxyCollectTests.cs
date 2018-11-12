namespace Hypermedia.AspNetCore.Siren.Test
{
    using System.Linq;
    using System.Reflection;
    using Endpoints;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
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
        private void TestPerformanceOld(ExpressionCallCollector sut)
        {
            var callCollected = sut.CollectMethodCall<Collectable>(e => e.CollectMe(1, 2, 3 + 2));

            Assert.Equal(typeof(Collectable), callCollected.Target);
            Assert.Equal(callCollected.Method, this.CollectMeMethodInfo);
            Assert.Equal(callCollected.Arguments.OfType<int>(), new [] { 1, 2, 5 });
        }
    }
}
