using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Interfaces;

namespace VanakIndustry.Web.Initializers
{
   public static class ServicesByConventionConfigurator
    {
        public static IServiceCollection ConfigureServicesByConvention(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = GetAllTypes(assemblies);

            RegisterSingletonServices(services, types);

            RegisterScopedServices(services, types);

            RegisterTransientServices(services, types);

            RegisterApiHandlers(services, types);

            RegisterValidators(services, types);

            return services;
        }

        private static void RegisterSingletonServices(IServiceCollection services, List<Type> types)
        {
            foreach (var type in types.Where(x => !x.IsAbstract && typeof(ISingleton).IsAssignableFrom(x)))
            {
                services.AddSingleton(type);
            }
        }

        private static void RegisterScopedServices(IServiceCollection services, List<Type> types)
        {
            foreach (var type in types.Where(x => !x.IsAbstract && typeof(IScoped).IsAssignableFrom(x)))
            {
                services.AddScoped(type);
            }
        }

        private static void RegisterTransientServices(IServiceCollection services, List<Type> types)
        {
            foreach (var type in types.Where(x => !x.IsAbstract && typeof(ITransient).IsAssignableFrom(x)))
            {
                services.AddTransient(type);
            }
        }

        private static void RegisterApiHandlers(IServiceCollection services, List<Type> types)
        {
            foreach (var type in types.Where(x => !x.IsAbstract && typeof(IApiRequestHandler).IsAssignableFrom(x)))
            {
                var itype = type.GetInterfaces().First(x => x.GenericTypeArguments.Any());

                services.AddScoped(itype, type);
            }
        }

        private static void RegisterValidators(IServiceCollection services, List<Type> types)
        {
            foreach (var type in types.Where(x => typeof(IValidator).IsAssignableFrom(x)))
            {
                var itype = type.GetInterfaces().First(x => x.GenericTypeArguments.Any());

                services.AddScoped(itype, type);
            }
        }

        private static List<Type> GetAllTypes(Assembly[] assemblies)
        {
            var list = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().ToList();

                list.AddRange(types);
            }

            return list;
        }
    }
}
