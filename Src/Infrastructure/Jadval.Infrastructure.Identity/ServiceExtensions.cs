using Jadval.Domain.Settings;
using System;
using Jadval.Infrastructure.Identity.Contexts;
using Jadval.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TSPStore.Infrastructure.Identity
{
    public static class ServiceExtensions
    {
  
        public static void AddIdentityCookie(this IServiceCollection services, IConfiguration configuration)
        {
            var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
            services.AddSingleton(identitySettings);
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;

                options.Password.RequireDigit = identitySettings.PasswordRequireDigit;
                options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = identitySettings.PasswordRequireNonAlphanumic;
                options.Password.RequireUppercase = identitySettings.PasswordRequireUppercase;
                options.Password.RequireLowercase = identitySettings.PasswordRequireLowercase;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        }
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"),
                    b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

 
        }
    }
}
