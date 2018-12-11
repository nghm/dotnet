namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using AccessValidation;
    using Fields;
    using HrefProviders;

    internal class ProxifiedActionFactory : IProxifiedActionFactory
    {
        private readonly IHrefProvider _hrefProvider;
        private readonly IAccessValidator _validator;
        private readonly IFieldsFactory _fieldsFactory;

        public ProxifiedActionFactory(IHrefProvider hrefProviders, IAccessValidator validator, IFieldsFactory fieldsFactory)
        {
            this._hrefProvider = hrefProviders;
            this._validator = validator;
            this._fieldsFactory = fieldsFactory;
        }

        public IProxifiedAction Make(object[] arguments)
        {
            return new ProxifiedAction(
                this._hrefProvider,
                this._validator,
                this._fieldsFactory,
                arguments
            );
        }
    }
}