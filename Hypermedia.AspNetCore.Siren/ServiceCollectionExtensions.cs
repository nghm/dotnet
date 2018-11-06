namespace Hypermedia.AspNetCore.Siren
{
    using Actions;
    using Actions.Fields;
    using Actions.Fields.Type;
    using Actions.Fields.Validation;
    using Microsoft.Extensions.DependencyInjection;
    using ProxyCollectors;

    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;
            
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<ICallCollector, ExpressionCallCollector>();
            services.AddSingleton<IEndpointDescriptorProvider, EndpointDescriptorProvider>();
            services.AddSingleton<IEntityBuilderFactory, EntityBuilderFactory>();

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
