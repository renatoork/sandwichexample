using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sandwish.Server.HandlerException;
using Sandwish.Server.Repository;
using Sandwish.Server.Service;
using Swashbuckle.AspNetCore.Swagger;

namespace Sandwish.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SandwishDbContext>(options => options.UseInMemoryDatabase(databaseName: "Sandwish"));
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddSingleton<SandwishCore>();

            services.AddScoped<SandwishService>();
            services.AddScoped<RegisterService>();
            services.AddScoped<ISandwishRepository, SandwishRepository>();
            services.AddScoped<IRegisterRepository, RegisterRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Sandwish", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

         public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOption.Value);

            app.UseCors(builder =>
            {
                builder.WithOrigins("*")
                       .WithMethods("*")
                       .AllowAnyHeader();
            });

            app.UseExceptionHandlerLocalizer();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandwish V1");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
