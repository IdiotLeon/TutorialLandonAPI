using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TutorialLandonAPI.Infrastructure;
using TutorialLandonAPI.Filters;

namespace TutorialLandonAPI
{
    public class Startup
    {

        private int? _httpsPort;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {

                opt.Filters.Add(typeof(JsonExceptionFilter));

                // To require HTTPS for all contributors
                opt.SslPort = _httpsPort;
                opt.Filters.Add(typeof(RequireHttpsAttribute));

                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single();
                opt.OutputFormatters.Remove(jsonFormatter);
                opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // To get the HTTPS port (only in development)
                var launchJsonConfig = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("Properties/launchSettings.json")
                    .Build();

                _httpsPort = launchJsonConfig.GetValue<int>("iisSettings:iisExpress::sslPort");
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseHsts(opt =>{
                opt.MaxAge(days: 180);
                opt.IncludeSubdomains();
                opt.Preload();
            });
            app.UseMvc();
        }
    }
}
