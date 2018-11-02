namespace Hypermedia.WebApi.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;

    public class AutoMockCrudServiceSeed<T, TKey> : ICrudServiceSeed<T, TKey> where T : class, IEntity<TKey>
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly ICrudService<T, TKey> _crudService;

        public AutoMockCrudServiceSeed(ICrudService<T, TKey> crudService)
        {
            this._crudService = crudService;
            this._fixture.Customize(new AutoMoqCustomization());
        }

        public void Seed(int amount)
        {
            Infinite()
                .Take(amount)
                .ToList()
                .ForEach(id =>
                {
                    this._crudService.Create(this._fixture.Create<T>());
                });
        }

        private static IEnumerable<int> Infinite(int value = 0)
        {
            do
            {
                yield return value++;
            } while (value > 0);
        }
    }
}
