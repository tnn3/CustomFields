using System.Reflection;
using DAL;
using DAL.Repositories;
using Domain;
using Interfaces;
using Interfaces.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                options.UseInMemoryDatabase("CustomFields"));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IDbContext, ApplicationDbContext>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddMvc();

            var embeddedFileProvider = new EmbeddedFileProvider(typeof(FormFactory.FF).GetTypeInfo().Assembly, nameof(FormFactory));
            //Add the file provider to the Razor view engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("~/bin/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.FileProviders.Add(embeddedFileProvider);
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

            var options = new StaticFileOptions
            {
                RequestPath = "",
                FileProvider = new EmbeddedFileProvider(typeof(FormFactory.FF).GetTypeInfo().Assembly, nameof(FormFactory))
            };

            app.UseStaticFiles(options);

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
