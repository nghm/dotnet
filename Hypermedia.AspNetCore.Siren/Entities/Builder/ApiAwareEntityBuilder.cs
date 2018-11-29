namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Parallel;
    using Steps;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    internal class ApiAwareEntityBuilder : IApiAwareEntityBuilder
    {

        private readonly ParallelBuildingEnvironment<IEntityBuilder, IEntity> _environment;

        public ApiAwareEntityBuilder(
            ParallelBuildingEnvironment<IEntityBuilder, IEntity> environment
        )
        {
            this._environment = environment;
        }

        public IApiAwareEntityBuilder WithEntity(Action<IApiAwareEntityBuilder> configureBuilder)
        {
            this._environment.AddParallelBuildStep<AddEmbeddedEntityStep>(step => step.Configure(configureBuilder));

            return this;
        }


        public IApiAwareEntityBuilder WithEntity<T>(Expression<Action<T>> capturedExpression, params string[] classes)
            where T : class
        {
            this._environment.AddParallelBuildStep<AddLinkedEntityStep<T>>(step => step.Configure(capturedExpression, classes));

            return this;
        }
        public IApiAwareEntityBuilder WithEntities<TM>(IEnumerable<TM> enumerable, Action<IApiAwareEntityBuilder, TM> configureOne)
        {
            foreach (var enumeration in enumerable)
            {
                this.WithEntity(builder => configureOne(builder, enumeration));
            }

            return this;
        }

        public IApiAwareEntityBuilder WithEntities<TController, TM>(IEnumerable<TM> enumerable, Action<TController, TM> configureOne,
            string[] classes) where TController : class
        {
            foreach (var enumeration in enumerable)
            {
                this.WithEntity((TController controller) => configureOne(controller, enumeration), classes);
            }

            return this;
        }

        public IApiAwareEntityBuilder WithProperties<TProps, TSource>(TSource properties)
        {
            this._environment.AddParallelBuildStep<AddMappedSourcePropertiesStep<TProps, TSource>>(step => step.Configure(properties));

            return this;
        }
        public IApiAwareEntityBuilder WithProperties<TProps>(TProps properties)
        {
            this._environment.AddParallelBuildStep<AddSourcePropertiesStep<TProps>>(step => step.Configure(properties));

            return this;
        }

        public IApiAwareEntityBuilder WithClasses(params string[] classes)
        {
            this._environment.AddParallelBuildStep<AddClassesStep>(step => step.Configure(classes));

            return this;
        }

        public IApiAwareEntityBuilder WithLink<TController>(string name, Expression<Action<TController>> captureExpression, params string[] rel)
            where TController : class
        {
            this._environment.AddParallelBuildStep<AddLinkBuildStep<TController>>(step => step.Configure(name, captureExpression, rel));

            return this;
        }

        public IApiAwareEntityBuilder WithLinks<TController>(string[] rel, IDictionary<string, Expression<Action<TController>>> links)
            where TController : class
        {
            foreach (var link in links)
            {
                WithLink(link.Key, link.Value, rel);
            }

            return this;
        }

        public IApiAwareEntityBuilder WithLinks<TController>(IDictionary<string, Expression<Action<TController>>> links) where TController : class
        {
            foreach (var link in links)
            {
                WithLink(link.Key, link.Value);
            }

            return this;
        }

        public IApiAwareEntityBuilder WithAction<TController, TBody>(
            string name,
            Expression<Action<TController>> endpointCapture,
            Action<IActionBuilder<TBody>> configureActionBuilder = null
        )
            where TController : class
            where TBody : class
        {
            this._environment.AddParallelBuildStep<AddActionBuildStep<TController, TBody>>(
                step => step.Configure(name, endpointCapture, configureActionBuilder)
            );

            return this;
        }


        public async Task<IEntity> BuildAsync()
        {
            return await this._environment.BuildAsync();
        }
    }

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