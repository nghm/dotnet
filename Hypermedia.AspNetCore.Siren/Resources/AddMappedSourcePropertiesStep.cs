namespace Hypermedia.AspNetCore.Siren.Resources
{
    using AutoMapper;
    using Environments;
    using Hypermedia.AspNetCore.Siren.Entities;
    using System.Threading.Tasks;

    internal class AddMappedSourcePropertiesStep<TProps> : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private readonly IMapper _mapper;
        private object _properties;

        public AddMappedSourcePropertiesStep(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public void Configure(object properties)
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