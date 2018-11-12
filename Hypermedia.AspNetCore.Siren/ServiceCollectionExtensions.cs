namespace Hypermedia.AspNetCore.Siren
{
    using Actions;
    using Actions.Fields;
    using Actions.Fields.Type;
    using Actions.Fields.Validation;
    using Endpoints;
    using Entities.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddTransient<EntityBuilder>();
            services.AddSingleton<IAccessValidator, AccessValidator>();
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<ICallCollector, ExpressionCallCollector>();
            services.AddSingleton<IEndpointDescriptorProvider, EndpointDescriptorProvider>();
            services.AddSingleton<IHrefFactory, HrefFactory>();
            services.AddSingleton<IFieldsFactory, FieldsFactory>();

            services.AddSingleton(_ => new IValidationMetaProvider[]
            {
                new RequiredMetaProvider(),
                new PatternMetaProvider()
            });

            services.AddSingleton(_ => new ITypeMetaProvider[]
            { 
                new StringMetaProvider(),
                new NumberMetaProvider(),
                new OptionMetaProvider(), 
                new OptionsMetaProvider()
            });

            services.AddSingleton<TypeMetadataProvider>();
            services.AddSingleton<ValidationMetadataProvider>();
            services.AddSingleton<IFieldMetadataProviderCollection, FieldMetadataProviderCollection>();

            mvcBuilder.AddMvcOptions(options => { options.Filters.Add(typeof(HypermediaResourceFilter)); });

            return mvcBuilder;
        }
    }
}
