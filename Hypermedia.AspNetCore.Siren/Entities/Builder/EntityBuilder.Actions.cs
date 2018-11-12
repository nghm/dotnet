namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System;
    using System.Linq.Expressions;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithAction<T>(string name, Expression<Action<T>> endpointCapture) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture, this._claimsPrincipal);

            if (descriptor == null)
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

        public IEntityBuilder WithAction<T, TBody>(
            string name,
            Expression<Action<T>> endpointCapture,
            Action<IActionBuilder<TBody>> configureAction
        ) where T: class
          where TBody : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture, this._claimsPrincipal);

            if (descriptor == null)
            {
                return this;
            }
            
            var method = descriptor.Method;

            var href = this._hrefFactory.MakeHref(descriptor);
            var fields = this._fieldsFactory.MakeFields(descriptor.BodyArgument);

            var action = new Actions.Action(name, href, method, fields);

            var builder = new ActionBuilder<TBody>(action);

            configureAction(builder);

            this._actions.Add(action);

            return this;
        }
    }
}