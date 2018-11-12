namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithEntity<T>(Expression<Action<T>> endpointCapture, params string[] classes)
            where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture, this._claimsPrincipal);

            if (descriptor == null)
            {
                return this;
            }

            var method = descriptor.Method;
            var href = this._hrefFactory.MakeHref(descriptor);

            if (method == "GET")
            {
                this._entities.Add(new Entity(classes, href));
            }

            return this;
        }

        public IEntityBuilder WithEntity(Action<IEntityBuilder> configure)
        {
            var builder = EmptyClone();

            configure(builder);

            this._entities.Add(builder.Build());

            return this;
        }

        public IEntityBuilder WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne)
        {
            foreach (var enumeration in enumerable)
            {
                var builder = EmptyClone();

                configureOne(builder, enumeration);

                this._entities.Add(builder.Build());
            }

            return this;
        }

        public IEntityBuilder WithEntities<T, TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne,
            string[] classes) where T : class
        {
            foreach (var enumeration in enumerable)
            {
                this.WithEntity((T controller) => configureOne(controller, enumeration), classes);
            }

            return this;
        }
    }
}