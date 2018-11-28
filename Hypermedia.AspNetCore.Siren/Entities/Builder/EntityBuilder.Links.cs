namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Links;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithLink(ILink link)
        {
            this._links.Add(link);

            return this;
        }

        public IEntityBuilder WithLink<T>(string name, Expression<Action<T>> endpointCapture, params string[] rel)
            where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture, this._claimsPrincipal);

            if (descriptor == null)
            {
                return this;
            }

            if (descriptor.Body != null)
            {
                throw new InvalidOperationException("Cannot add link, body is not null!");
            }

            if (descriptor.Method != "GET")
            {
                throw new InvalidOperationException("Cannot add link, method is not GET!");
            }

            var href = this._hrefFactory.MakeHref(descriptor);

            return WithLink(new Link(name, href, rel));
        }

        public IEntityBuilder WithLinks<T>(string[] rel, IDictionary<string, Expression<Action<T>>> links)
            where T : class
        {
            foreach (var link in links)
            {
                WithLink(link.Key, link.Value, rel);
            }

            return this;
        }

        public IEntityBuilder WithLinks<T>(IDictionary<string, Expression<Action<T>>> links) where T : class
        {
            foreach (var link in links)
            {
                WithLink(link.Key, link.Value);
            }

            return this;
        }
    }
}