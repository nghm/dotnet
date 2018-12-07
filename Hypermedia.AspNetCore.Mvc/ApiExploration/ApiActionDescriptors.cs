namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using AutoMapper;
    using System;
    using System.Reflection;

    internal class ApiActionDescriptors : IApiActionDescriptors
    {
        private readonly IMapper _mapper;
        private readonly IMvcActionDescriptors _resolver;

        public ApiActionDescriptors(IMapper mapper, IMvcActionDescriptors resolver)
        {
            this._mapper = mapper;
            this._resolver = resolver;
        }

        public IApiActionDescriptor Get(MethodInfo action)
        {
            if (!this._resolver.TryGetValue(action, out var descriptor))
            {
                throw new InvalidOperationException("Method is not a known controller action!");
            }

            return this._mapper.Map<ApiActionDescriptor>(descriptor);
        }
    }
}