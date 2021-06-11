using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VanakIndustry.Web.Identity.Constants;
using VanakIndustry.Web.Identity.Handlers.Authorization;

namespace VanakIndustry.Web.Initializers
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options
                    .RequireActiveUser()
                    .DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(AuthSchemes.Jwt)
                    .Build();
            });

            services.AddScoped<IAuthorizationHandler, ActiveUserAuthorizationHandler>();

            return services;
        }
    }
}
