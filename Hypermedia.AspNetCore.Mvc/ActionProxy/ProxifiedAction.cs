namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using AccessValidation;
    using ApiExploration;
    using HrefProviders;
    using System.Security.Claims;

    internal class ProxifiedAction : IProxifiedAction
    {
        private readonly IHrefProvider _hrefProvider;
        private readonly IAccessValidator _accessValidator;
        private readonly IFieldsFactory _fieldsFactory;
        private readonly object[] _arguments;

        public ProxifiedAction(
            IHrefProvider hrefProvider,
            IAccessValidator accessValidator,
            IFieldsFactory fieldsFactory,
            object[] arguments
        )
        {
            this._hrefProvider = hrefProvider;
            this._accessValidator = accessValidator;
            this._fieldsFactory = fieldsFactory;
            this._arguments = arguments;
        }

        public string Href => this._hrefProvider.Get(this._arguments);
        public bool Allows(ClaimsPrincipal user) => this._accessValidator.Allows(user);
        public IFieldDescriptor[] FieldDescriptors => this._fieldsFactory.Make(this._arguments);
    }
}