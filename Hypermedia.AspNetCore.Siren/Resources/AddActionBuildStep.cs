namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Actions.Fields;
    using Endpoints;
    using Hypermedia.AspNetCore.Siren.Actions;
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Builder;

    internal class AddActionBuildStep<TController, TBody> : IAsyncBuildStep<IEntityBuilder, IEntity>
        where TController : class
        where TBody : class
    {
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefFactory _hrefFactory;
        private readonly IFieldsFactory _fieldsFactory;
        private readonly ClaimsPrincipal _claimsPrincipal;

        private Expression<Action<TController>> _captureExpression;
        private string _name;
        private Action<IActionBuilder<TBody>> _configureActionBuilder;

        public AddActionBuildStep(
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            IFieldsFactory fieldsFactory,
            ClaimsPrincipal claimsPrincipal)
        {
            this._endpointDescriptorProvider = endpointDescriptorProvider ?? throw new ArgumentNullException(nameof(endpointDescriptorProvider));
            this._claimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));
            this._hrefFactory = hrefFactory ?? throw new ArgumentNullException(nameof(hrefFactory));
            this._fieldsFactory = fieldsFactory ?? throw new ArgumentNullException(nameof(fieldsFactory));
        }

        public void Configure(
            string name,
            Expression<Action<TController>> captureExpression,
            Action<IActionBuilder<TBody>> configureActionBuilder
        )
        {
            this._captureExpression = captureExpression ?? throw new ArgumentNullException(nameof(captureExpression));
            this._name = name ?? throw new ArgumentNullException(nameof(name));
            this._configureActionBuilder = configureActionBuilder;
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var endpointDescriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(this._captureExpression, this._claimsPrincipal);

            if (endpointDescriptor == null)
            {
                return Task.CompletedTask;
            }

            var method = endpointDescriptor.Method;
            var href = this._hrefFactory.MakeHref(endpointDescriptor);
            var fields = this._fieldsFactory.MakeFields(endpointDescriptor.BodyArgument);

            var action = new Actions.Action(this._name, href, method, fields);

            if (this._configureActionBuilder != null)
            {
                var actionBuilder = new ActionBuilder<TBody>(action);

                this._configureActionBuilder(actionBuilder);
            }

            builder.WithAction(action);

            return Task.CompletedTask;
        }
    }
}