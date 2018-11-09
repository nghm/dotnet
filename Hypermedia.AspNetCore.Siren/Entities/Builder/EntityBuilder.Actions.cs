namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System;
    using System.Linq.Expressions;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithAction<T>(string name, Expression<Action<T>> endpointCapture) where T : class
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

            var method = descriptor.Method;

            var href = this._hrefFactory.MakeHref(descriptor);
            var fields = this._fieldsFactory.MakeFields(descriptor.BodyArgument);

            var action = new Actions.Action(name, href, method, fields);

            this._actions.Add(action);

            return this;
        }
    }
}