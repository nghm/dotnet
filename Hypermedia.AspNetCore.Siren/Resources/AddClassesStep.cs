namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Builder;
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Threading.Tasks;

    internal class AddClassesStep : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private string[] _classes;

        public void Configure(string[] classes)
        {
            this._classes = classes ?? throw new ArgumentNullException(nameof(classes));
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.WithClasses(this._classes);

            return Task.CompletedTask;
        }
    }
}