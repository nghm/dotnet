using Hypermedia.AspNetCore.Siren.Actions.Fields;

namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Actions;
    using System;
    using System.Linq.Expressions;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithAction(IAction action)
        {
            this._actions.Add(action);

            return this;
        }

        public IEntityBuilder WithAction<T>(string name, Expression<Action<T>> endpointCapture) where T : class
        {
            var endpointDescriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture, this._claimsPrincipal);

            if (endpointDescriptor == null)
            {
                return this;
            }

            var method = endpointDescriptor.Method;

            var href = this._hrefFactory.MakeHref(endpointDescriptor);
            var fields = this._fieldsFactory.MakeFields(new ActionArgument(endpointDescriptor.BodyArgument.Descriptor, endpointDescriptor.BodyArgument.Value));

            return WithAction(new Actions.Action(name, href, method, fields));
        }

        public IEntityBuilder WithAction<T, TBody>(
            string name,
            Expression<Action<T>> endpointCapture,
            Action<IActionBuilder<TBody>> configureAction
        ) where T : class
          where TBody : class
        {
            var endpointDescriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(endpointCapture, this._claimsPrincipal);

            if (endpointDescriptor == null)
            {
                return this;
            }

            var method = endpointDescriptor.Method;

            var href = this._hrefFactory.MakeHref(endpointDescriptor);
            var fields = this._fieldsFactory.MakeFields(new ActionArgument(endpointDescriptor.BodyArgument.Descriptor, endpointDescriptor.BodyArgument.Value));

            var action = new Actions.Action(name, href, method, fields);

            var builder = new ActionBuilder<TBody>(action);

            configureAction(builder);

            this._actions.Add(action);

            return this;
        }
    }
}