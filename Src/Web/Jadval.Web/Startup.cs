using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Jadval.Application;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using TSPStore.Infrastructure.Identity;
using Jadval.Application.Interfaces;
using Jadval.Web.Infrastracture.Services;
using Jadval.Infrastructure.Persistence;
using Jadval.Web.Infrastracture.Middlewares;

namespace Jadval.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddDistributedMemoryCache();
            services.AddIdentityInfrastructure(Configuration);
            services.AddIdentityCookie(Configuration);

            services.AddControllersWithViews().AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
