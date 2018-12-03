namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Endpoints;
    using Environments;
    using Hypermedia.AspNetCore.Siren.Entities;
    using Links;
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading.Tasks;

    internal class AddLinkBuildStep<TController> : IAsyncBuildStep<IEntityBuilder, IEntity>
        where TController : class
    {
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefFactory _hrefFactory;
        private readonly ClaimsPrincipal _claimsPrincipal;

        private Expression<Action<TController>> _captureExpression;
        private string _name;
        private string[] _rel;

        public AddLinkBuildStep(
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            ClaimsPrincipal claimsPrincipal)
        {
            this._endpointDescriptorProvider = endpointDescriptorProvider;
            this._hrefFactory = hrefFactory;
            this._claimsPrincipal = claimsPrincipal;
        }

        public void Configure(
            string name,
            Expression<Action<TController>> captureExpression,
            string[] rel = null
        )
        {
            this._captureExpression = captureExpression;
            this._name = name;
            this._rel = rel;
        }

        public async Task BuildAsync(IEntityBuilder builder)
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(this._captureExpression, this._claimsPrincipal);

            if (descriptor == null)
            {
                await Task.CompletedTask;

                return;
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

            builder.WithLink(new Link(this._name, href, this._rel));
        }
    }
}