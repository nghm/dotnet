namespace Hypermedia.AspNetCore.Siren.Entities.Builder.Steps
{
    using AutoMapper;
    using Parallel;
    using System.Threading.Tasks;

    internal class AddMappedSourcePropertiesStep<TProps, TSource> : IParallelBuildStep<IEntityBuilder, IEntity>
    {
        private readonly IMapper _mapper;
        private TSource _properties;

        public AddMappedSourcePropertiesStep(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public void Configure(TSource properties)
        {
            this._properties = properties;
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            builder.WithProperties(this._mapper.Map<TProps>(this._properties));

            return Task.CompletedTask;
        }
    }
}