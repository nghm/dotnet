using Hypermedia.AspNetCore.AsyncStepBuilder;

namespace Hypermedia.AspNetCore.Siren.Resources
{
    using AutoMapper;
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Threading.Tasks;

    internal class AddMappedSourcePropertiesStep<TProps> : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private readonly IMapper _mapper;
        private object _properties;

        public AddMappedSourcePropertiesStep(IMapper mapper)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Configure(object properties)
        {
            this._properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.WithProperties(this._mapper.Map<TProps>(this._properties));

            return Task.CompletedTask;
        }
    }
}