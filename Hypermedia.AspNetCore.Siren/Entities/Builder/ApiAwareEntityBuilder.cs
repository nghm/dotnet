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

        public ApiAwareEntityBuilder(ParallelBuildingEnvironment<IEntityBuilder, IEntity> environment)
        {
            this._environment = environment;
        }

        public IApiAwareEntityBuilder WithAction<TController, TBody>(
            string name,
            Expression<Action<TController>> resource,
            Action<IActionBuilder<TBody>> configureActionBuilder = null
        ) where TController : class
          where TBody : class
        {
            this._environment.AddParallelBuildStep<AddActionBuildStep<TController, TBody>>(
                step => step.Configure(name, resource, configureActionBuilder)
            );

            return this;
        }
        public IApiAwareEntityBuilder WithAction<TController>(
            string name,
            Expression<Action<TController>> resource
        ) where TController : class
        {
            this._environment.AddParallelBuildStep<AddActionBuildStep<TController, object>>(
                step => step.Configure(name, resource, null)
            );

            return this;
        }

        public IApiAwareEntityBuilder WithActions<TController, TBody>(
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

        public IApiAwareEntityBuilder WithLink<TController>(
            string name,
            Expression<Action<TController>> resource,
            string[] rel = null
        )
            where TController : class
        {
            this._environment.AddParallelBuildStep<AddLinkBuildStep<TController>>(
                step => step.Configure(name, resource, rel)
            );

            return this;
        }

        public IApiAwareEntityBuilder WithLinks<TController>(
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

        public IApiAwareEntityBuilder WithEmbeddedEntity(Action<IApiAwareEntityBuilder> newEntity)
        {
            this._environment.AddParallelBuildStep<AddEmbeddedEntityStep>(step => step.Configure(newEntity));

            return this;
        }

        public IApiAwareEntityBuilder WithEmbeddedEntities(params Action<IApiAwareEntityBuilder>[] newEntities)
        {
            foreach (var newEntity in newEntities)
            {
                this.WithEmbeddedEntity(newEntity);
            }

            return this;
        }

        public IApiAwareEntityBuilder WithEmbeddedEntitiesForEach<TOne>(
            IEnumerable<TOne> each,
            Action<IApiAwareEntityBuilder, TOne> newEntityForOne
        )
        {
            foreach (var one in each)
            {
                this.WithEmbeddedEntity(builder => newEntityForOne(builder, one));
            }

            return this;
        }

        public IApiAwareEntityBuilder WithLinkedEntity<TController>(
            Expression<Action<TController>> resource,
            string[] classes = null
        )
            where TController : class
        {
            this._environment.AddParallelBuildStep<AddLinkedEntityStep<TController>>(step => step.Configure(resource, classes));

            return this;
        }

        public IApiAwareEntityBuilder WithLinkedEntities<TController>(
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

        public IApiAwareEntityBuilder WithLinkedEntities<TController>(
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

        public IApiAwareEntityBuilder WithLinkedEntitiesForEach<TController, TOne>(
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
        public IApiAwareEntityBuilder WithLinkedEntitiesForEach<TController, TOne>(
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

        public IApiAwareEntityBuilder WithProperties<TProps>(object properties)
        {
            this._environment.AddParallelBuildStep<AddMappedSourcePropertiesStep<TProps>>(step => step.Configure(properties));

            return this;
        }
        public IApiAwareEntityBuilder WithProperties(object properties)
        {
            this._environment.AddParallelBuildStep<AddSourcePropertiesStep>(step => step.Configure(properties));

            return this;
        }

        public IApiAwareEntityBuilder WithClasses(params string[] classes)
        {
            this._environment.AddParallelBuildStep<AddClassesStep>(step => step.Configure(classes));

            return this;
        }

        public async Task<IEntity> BuildAsync()
        {
            return await this._environment.BuildAsync();
        }
    }
}