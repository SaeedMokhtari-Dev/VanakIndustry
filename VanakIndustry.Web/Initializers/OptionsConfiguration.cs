using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VanakIndustry.Web.Configuration.Constants;
using VanakIndustry.Web.Configuration.Models;

namespace VanakIndustry.Web.Initializers
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BCryptOptions>(configuration.GetSection(SettingsKeys.BCrypt));
            services.Configure<SmtpOptions>(configuration.GetSection(SettingsKeys.Smtp));

            return services;
        }
    }
}
