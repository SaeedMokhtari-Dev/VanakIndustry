using System;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.DependencyInjection;

namespace VanakIndustry.Web.Initializers
{
    public static class HstsConfiguration
    {
        public static IServiceCollection ConfigureHsts(this IServiceCollection services)
        {
            services.Configure<HstsOptions>(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            return services;
        }
    }
}
