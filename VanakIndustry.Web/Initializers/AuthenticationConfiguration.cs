using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VanakIndustry.Web.Configuration.Constants;
using VanakIndustry.Web.Identity.Constants;
using VanakIndustry.Web.Identity.Models;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Initializers
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = GetTokenOptions(configuration);

            services.AddSingleton(new AccessTokenService(tokenOptions));

            services
                .AddAuthentication(AuthSchemes.Jwt)
                .AddJwtBearer(AuthSchemes.Jwt, options =>
                {
                    options.TokenValidationParameters = tokenOptions.TokenValidationParameters;
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            if (string.IsNullOrEmpty(accessToken) == false)
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        },
                    };
                });

            return services;
        }

        private static TokenOptions GetTokenOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(nameof(SettingsKeys.Jwt));

            var securityKey = GetSecurityKey(section);
            var issuer = section.GetValue<string>(SettingsKeys.Jwt.Issuer);
            var audience = section.GetValue<string>(SettingsKeys.Jwt.Audience);

            return new TokenOptions
            {
                AccessTokenExpireTime = IdentitySettings.AccessTokenExpireTime,
                RefreshTokenExpireTime = IdentitySettings.RefreshTokenExpireTime,

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey,
                    ClockSkew = IdentitySettings.AccessTokenValidationClockSkew
                },
                RefreshAccessTokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.Zero,
                }
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration jwtSection)
        {
            var securityKey = jwtSection.GetValue<string>(SettingsKeys.Jwt.SecurityKey);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            return symmetricSecurityKey;
        }
    }
}
