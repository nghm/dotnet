namespace Hypermedia.AspNetCore.Siren
{
    using Actions;
    using Actions.Fields;
    using Actions.Fields.Type;
    using Actions.Fields.Validation;
    using Endpoints;
    using Entities.Builder;
    using Entities.Builder.Steps;
    using Environments;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddTransient<ResourceBuilder>();
            services.AddSingleton<IAccessValidator, AccessValidator>();
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<ICallCollector, ExpressionCallCollector>();
            services.AddSingleton<IEndpointDescriptorProvider, EndpointDescriptorProvider>();
            services.AddSingleton<IHrefFactory, HrefFactory>();
            services.AddSingleton<IFieldsFactory, FieldsFactory>();
            services.AddSingleton(typeof(IScopedBuildApplier<,>), typeof(ScopedBuildApplier<,>));

            services.AddAsyncBuildSteps();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddTransient<IEntityBuilder, EntityBuilder>();
            services.AddTransient(typeof(IStorage<>), typeof(Storage<>));
            services.AddTransient(typeof(IAsyncBuildingEnvironment<,>), typeof(AsyncBuildingEnvironment<,>));
            services.AddTransient<IResourceBuilder, ResourceBuilder>();

            services.AddSingleton(_ => new IValidationMetaProvider[]
            {
                new RequiredMetaProvider(),
                new PatternMetaProvider()
            });

            services.AddSingleton(serviceProvider => new ITypeMetaProvider[]
            {
                serviceProvider.GetService<StringMetaProvider>(),
                serviceProvider.GetService<NumberMetaProvider>(),
                serviceProvider.GetService<OptionMetaProvider>(),
                new OptionsMetaProvider()
            });

            services.AddSingleton<TypeMetadataProvider>();
            services.AddSingleton<ValidationMetadataProvider>();
            services.AddSingleton<IFieldMetadataProviderCollection, FieldMetadataProviderCollection>();

            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(HypermediaResourceFilter));
            });

            return mvcBuilder;
        }

        private static void AddAsyncBuildSteps(this IServiceCollection services)
        {
            services.AddTransient(typeof(AddActionBuildStep<,>));
            services.AddTransient<AddClassesStep>();
            services.AddTransient<AddEmbeddedEntityStep>();
            services.AddTransient<AddSourcePropertiesStep>();
            services.AddTransient(typeof(AddMappedSourcePropertiesStep<>));
            services.AddTransient<AddSourcePropertiesStep>();
            services.AddTransient(typeof(AddActionBuildStep<,>));
            services.AddTransient(typeof(AddLinkedEntityStep<>));
            services.AddTransient(typeof(AddLinkBuildStep<>));
        }
    }
}