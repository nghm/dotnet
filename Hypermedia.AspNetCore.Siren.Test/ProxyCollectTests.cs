namespace Hypermedia.AspNetCore.Siren.Test
{
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Xunit;
    using ProxyCollectors;

    public class ProxyCollectTests
    {
        [Theory]
        [AutoMockData]
        private void TestPerformanceOld(ExpressionProxyCollector sut)
        {
            for (var i = 0; i < 10000; i++)
            {
                sut.ProxyCollectOne<ProxyCollectTests>(e => ComplexMethod(i + 1, i + 2, i * i));
            }
        }
        
        void ComplexMethod(int a, int b, int c = 0)
        {
        }
    }
}
