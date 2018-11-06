namespace Books.WebApi
{
    using System;
    using System.Security.Claims;
    using AutoMapper;
    using Hypermedia.AspNetCore.Siren;
    using Infrastructure;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc()
                .AddHypermediaSiren()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();
            
            services.AddSingleton(typeof(ICrudServiceSeed<,>), typeof(AutoMockCrudServiceSeed<,>));
            services.AddSingleton(typeof(IGuidGenerator<>), typeof(AutoMockGuidGenerator<>));
            services.AddSingleton(typeof(ICrudService<,>), typeof(InMemoryCrudService<,>));
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanEditBooks", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });

                options.AddPolicy("CanCreateBooks", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:44363";
                    options.Audience = "api";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors(options => {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            app.UseMvc();

            app.UseMockSeeds<Book, Guid>(40);
        }
    }
}
