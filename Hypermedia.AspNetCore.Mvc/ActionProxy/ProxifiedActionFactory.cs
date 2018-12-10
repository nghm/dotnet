namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using AccessValidation;
    using HrefProviders;

    internal class ProxifiedActionFactory : IProxifiedActionFactory
    {
        private readonly IHrefProvider _provider;
        private readonly IAccessValidator _validator;


        public ProxifiedActionFactory(IHrefProvider providers, IAccessValidator validator)
        {
            this._provider = providers;
            this._validator = validator;
        }

        public IProxifiedAction Make(object[] arguments)
        {
            return new ProxifiedAction(
                this._provider,
                this._validator,
                arguments
            );
        }
    }
}