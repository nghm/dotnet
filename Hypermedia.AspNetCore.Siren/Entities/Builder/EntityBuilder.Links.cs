﻿namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Links;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithLink<T>(string name, Expression<Action<T>> endpointCapture, params string[] rel)
            where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture);

            if (descriptor == null)
            {
                return this;
            }

            if (!this._accessValidator.CanAccess(this._claimsPrincipal, descriptor.Policies))
            {
                return this;
            }

            if (!(descriptor.Body == null && descriptor.Method == "GET"))
            {
                return this;
            }

            this._links.Add(new Link(name, this._hrefFactory.MakeHref(descriptor), rel));

            return this;
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