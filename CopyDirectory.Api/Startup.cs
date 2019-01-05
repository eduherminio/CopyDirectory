using CopyDirectoryLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CopyDirectory.Internal.Exceptions;
using CopyDirectory.Api.Swagger;
using System.Reflection;

namespace CopyDirectory.Api
{
    public class Startup
    {
        private static readonly ApiVersion _apiVersion = new ApiVersion(1, 0);

        private ApiInfo ApiInfo => new ApiInfo(
            "CopyDirectory Public API",
            "CopyDirectory Public API",
            _apiVersion);

        private string SwaggerDocumentVersion { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SwaggerDocumentVersion = $"v{ApiInfo.ApiVersion.ToString()}";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddOptions();

            // Api versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiInfo.ApiVersion;
            });
            services.AddMvcCore().AddApiExplorer();

            // Swagger
            string assemblyName = GetType().GetTypeInfo().Assembly.GetName().Name;

            services.ConfigureSwaggerMvcServices(SwaggerDocumentVersion, ApiInfo, assemblyName);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.AddConsole();
            });

            ConfigureSpecificService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureSwaggerMvc(SwaggerDocumentVersion);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureSpecificService(IServiceCollection services)
        {
            services.AddCopyDirectoryServices();
        }
    }
}
