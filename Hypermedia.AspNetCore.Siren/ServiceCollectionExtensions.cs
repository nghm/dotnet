namespace Hypermedia.AspNetCore.Siren
{
    using Actions;
    using Actions.Fields;
    using Actions.Fields.Type;
    using Actions.Fields.Validation;
    using Builder;
    using Builders.Abstractions;
    using Endpoints;
    using Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Resources;
    using Store;

    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddSingleton<IAccessValidator, AccessValidator>();
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<IMethodCallPlucker, MethodMethodCallPlucker>();
            services.AddSingleton<IEndpointDescriptorProvider, EndpointDescriptorProvider>();
            services.AddSingleton<IHrefFactory, HrefFactory>();
            services.AddSingleton<IFieldsFactory, FieldsFactory>();

            services.AddAsyncBuildSteps();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddTransient<IEntityBuilder, EntityBuilder>();

            services.AddHypermediaStorage();
            services.AddHypermediaAsyncBuilder();

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