namespace Hypermedia.AspNetCore.Siren.Entities.Builder.Steps
{
    using Parallel;
    using System.Threading.Tasks;

    internal class AddClassesStep : IParallelBuildStep<IEntityBuilder, IEntity>
    {
        private string[] _classes;

        public void Configure(string[] classes)
        {
            this._classes = classes;
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            builder.WithClasses(this._classes);

            return Task.CompletedTask;
        }
    }
}