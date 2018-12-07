using Hypermedia.AspNetCore.AsyncStepBuilder;

namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Threading.Tasks;

    internal class AddSourcePropertiesStep : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private object _properties;

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

            builder.WithProperties(this._properties);

            return Task.CompletedTask;
        }
    }
}