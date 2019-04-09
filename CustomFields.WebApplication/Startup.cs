using System.Collections.Generic;
using System.Globalization;
using CustomFields.Resources;
using DAL;
using DAL.Repositories;
using Domain;
using Interfaces.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

//For Resharper/Raider to find custom field view paths
[assembly: AspMvcViewLocationFormat(@"~/../CustomFields")]

namespace WebApplication
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CustomFields"));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();

            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("et-EE"),
                    };

                options.DefaultRequestCulture = new RequestCulture(culture: "et-EE", uiCulture: "et-EE");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(CustomField));
                });

            services.ConfigureOptions(typeof(CustomFields.StaticFileConfiguration));

            //Add the custom field view locations to the Razor view engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("~/bin/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("~/bin/Views/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ProjectTask}/{action=Index}/{id?}");
            });
        }
    }
}
