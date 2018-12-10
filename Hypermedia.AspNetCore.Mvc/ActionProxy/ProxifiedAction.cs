namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using Siren.Builders.Abstractions;
    using System.Security.Claims;
    using AccessValidation;
    using ApiExploration;
    using HrefProviders;

    internal class ProxifiedAction : IProxifiedAction
    {
        private readonly IHrefProvider _hrefProvider;
        private readonly IAccessValidator _accessValidator;
        private readonly object[] _arguments;

        public ProxifiedAction(
            IHrefProvider hrefProvider,
            IAccessValidator accessValidator,
            object[] arguments
        )
        {
            this._hrefProvider = hrefProvider;
            this._accessValidator = accessValidator;
            this._arguments = arguments;
        }

        public string Href => this._hrefProvider.Get(this._arguments);
        public bool Allows(ClaimsPrincipal user) => this._accessValidator.Allows(user);
        public IFieldDescriptor[] Fields => this._fieldDescriptors.Get()
    }
}