namespace Hypermedia.AspNetCore.Mvc.AccessValidation
{
    using ApiExploration;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal class AccessValidators : IAccessValidators
    {
        private readonly IDictionary<IApiActionDescriptor, IAccessValidator> _validators = new ConcurrentDictionary<IApiActionDescriptor, IAccessValidator>();
        private readonly IAccessValidatorFactory _validatorFactory;

        public AccessValidators(IAccessValidatorFactory validatorFactory)
        {
            this._validatorFactory = validatorFactory;
        }

        public IAccessValidator Get(IApiActionDescriptor descriptor)
        {
            if (!this._validators.TryGetValue(descriptor, out var validator))
            {
                validator = this._validatorFactory.Make(descriptor.Policies);

                this._validators[descriptor] = validator;
            }

            return validator;
        }
    }
}
