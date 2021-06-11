using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace VanakIndustry.Web.Initializers
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Vanak Industry Portal",
                    Version = "v1",
                    Description = ""
                });

                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true);
                //c.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if(File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });
            
            /*services.AddSwaggerExamplesFromAssemblyOf<IModel>();
            services.AddSwaggerExamplesFromAssemblyOf<ControllerBase>();*/

            return services;
        }
    }
}
