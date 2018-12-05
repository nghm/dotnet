namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Builder;
    using Endpoints;
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading.Tasks;

    internal class AddLinkedEntityStep<TController> : IAsyncBuildStep<IEntityBuilder, IEntity> where TController : class
    {
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefFactory _hrefFactory;
        private readonly ClaimsPrincipal _claimsPrincipal;

        private string[] _classes;
        private Expression<Action<TController>> _capturedExpression;

        public AddLinkedEntityStep(
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            ClaimsPrincipal claimsPrincipal)
        {
            this._claimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));
            this._endpointDescriptorProvider = endpointDescriptorProvider ?? throw new ArgumentNullException(nameof(endpointDescriptorProvider));
            this._hrefFactory = hrefFactory ?? throw new ArgumentNullException(nameof(hrefFactory));
        }

        public void Configure(Expression<Action<TController>> capturedExpression, string[] classes = null)
        {
            this._capturedExpression = capturedExpression ?? throw new ArgumentNullException(nameof(capturedExpression));
            this._classes = classes;
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(this._capturedExpression, this._claimsPrincipal);

            if (descriptor == null)
            {
                return Task.CompletedTask;
            }

            var method = descriptor.Method;
            var href = this._hrefFactory.MakeHref(descriptor);

            if (method == "GET")
            {
                builder.WithEntity(new Entity(this._classes, href));
            }

            return Task.CompletedTask;
        }
    }
}