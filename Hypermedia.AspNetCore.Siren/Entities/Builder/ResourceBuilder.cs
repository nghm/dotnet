namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Environments;
    using Steps;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    internal class ResourceBuilder : IResourceBuilder
    {

        private readonly IAsyncBuildingEnvironment<IEntityBuilder, IEntity> _environment;

        public ResourceBuilder(IAsyncBuildingEnvironment<IEntityBuilder, IEntity> environment)
        {
            this._environment = environment;
        }

        public IResourceBuilder WithAction<TController, TBody>(
            string name,
            Expression<Action<TController>> resource,
            Action<IActionBuilder<TBody>> configureActionBuilder = null
        ) where TController : class
          where TBody : class
        {
            this._environment.AddAsyncBuildStep<AddActionBuildStep<TController, TBody>>(
                step => step.Configure(name, resource, configureActionBuilder)
            );

            return this;
        }
        public IResourceBuilder WithAction<TController>(
            string name,
            Expression<Action<TController>> resource
        ) where TController : class
        {
            this._environment.AddAsyncBuildStep<AddActionBuildStep<TController, object>>(
                step => step.Configure(name, resource, null)
            );

            return this;
        }

        public IResourceBuilder WithActions<TController, TBody>(
            params (
                string name,
                Expression<Action<TController>> resource,
                Action<IActionBuilder<TBody>> configure
                )[] actions
        )
            where TController : class
            where TBody : class
        {

            foreach (var (name, resource, configure) in actions)
            {
                WithAction(name, resource, configure);
            }

            return this;
        }

        public IResourceBuilder WithLink<TController>(
            string name,
            Expression<Action<TController>> resource,
            string[] rel = null
        )
            where TController : class
        {
            this._environment.AddAsyncBuildStep<AddLinkBuildStep<TController>>(
                step => step.Configure(name, resource, rel)
            );

            return this;
        }

        public IResourceBuilder WithLinks<TController>(
            params (
                string name,
                Expression<Action<TController>> resource,
                string[] rel
            )[] links
        )
            where TController : class
        {
            foreach (var (name, resource, rel) in links)
            {
                WithLink(name, resource, rel);
            }

            return this;
        }

        public IResourceBuilder WithEmbeddedEntity(Action<IResourceBuilder> newEntity)
        {
            this._environment.AddAsyncBuildStep<AddEmbeddedEntityStep>(step => step.Configure(newEntity));

            return this;
        }

        public IResourceBuilder WithEmbeddedEntities(params Action<IResourceBuilder>[] newEntities)
        {
            foreach (var newEntity in newEntities)
            {
                this.WithEmbeddedEntity(newEntity);
            }

            return this;
        }

        public IResourceBuilder WithEmbeddedEntitiesForEach<TOne>(
            IEnumerable<TOne> each,
            Action<IResourceBuilder, TOne> newEntityForOne
        )
        {
            foreach (var one in each)
            {
                this.WithEmbeddedEntity(builder => newEntityForOne(builder, one));
            }

            return this;
        }

        public IResourceBuilder WithLinkedEntity<TController>(
            Expression<Action<TController>> resource,
            string[] classes = null
        )
            where TController : class
        {
            this._environment.AddAsyncBuildStep<AddLinkedEntityStep<TController>>(step => step.Configure(resource, classes));

            return this;
        }

        public IResourceBuilder WithLinkedEntities<TController>(
            params (Expression<Action<TController>> resource, string[] classes)[] resources
        )
            where TController : class
        {
            foreach (var (resource, classes) in resources)
            {
                this.WithLinkedEntity(resource, classes);
            }

            return this;
        }

        public IResourceBuilder WithLinkedEntities<TController>(
            params Expression<Action<TController>>[] resources
        )
            where TController : class
        {
            foreach (var resource in resources)
            {
                this.WithLinkedEntity(resource);
            }

            return this;
        }

        public IResourceBuilder WithLinkedEntitiesForEach<TController, TOne>(
            IEnumerable<TOne> each,
            Func<TOne, (Expression<Action<TController>> resource, string[] classes)> linkedEntityForOne
        ) where TController : class
        {
            foreach (var one in each)
            {
                var (resource, classes) = linkedEntityForOne(one);

                this.WithLinkedEntity(resource, classes);
            }

            return this;
        }
        public IResourceBuilder WithLinkedEntitiesForEach<TController, TOne>(
            IEnumerable<TOne> each,
            Func<TOne, Expression<Action<TController>>> linkedEntityForOne
        ) where TController : class
        {
            foreach (var one in each)
            {
                var resource = linkedEntityForOne(one);

                this.WithLinkedEntity(resource);
            }

            return this;
        }

        public IResourceBuilder WithProperties<TProps>(object properties)
        {
            this._environment.AddAsyncBuildStep<AddMappedSourcePropertiesStep<TProps>>(step => step.Configure(properties));

            return this;
        }
        public IResourceBuilder WithProperties(object properties)
        {
            this._environment.AddAsyncBuildStep<AddSourcePropertiesStep>(step => step.Configure(properties));

            return this;
        }

        public IResourceBuilder WithClasses(params string[] classes)
        {
            this._environment.AddAsyncBuildStep<AddClassesStep>(step => step.Configure(classes));

            return this;
        }

        public async Task<IEntity> BuildAsync()
        {
            return await this._environment.BuildAsync();
        }
    }
}