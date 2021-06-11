using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace VanakIndustry.Web.Initializers
{
    public static class FormOptionsConfiguration
    {
        public static IServiceCollection ConfigureFormOptions(this IServiceCollection services)
        {
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            return services;
        }
    }
}
