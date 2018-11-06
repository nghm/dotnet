namespace Books.Infrastructure
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Services;

    public class AutoMockGuidGenerator<T> : IGuidGenerator<T>
    {
        private readonly IFixture _fixture = new Fixture();

        public AutoMockGuidGenerator()
        {
            this._fixture.Customize(new AutoMoqCustomization());
        }

        public T Make()
        {
            return this._fixture.Create<T>();
        }
    }
}