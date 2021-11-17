using Gateways.Core.Interfaces;
using Gateways.Core.Services.Settings;

using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGatewaySettings(this IServiceCollection services, GatewaySettings settings)
        {
            services.AddSingleton(settings);
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var serviceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsInstanceableAppService());

            foreach (var serviceType in serviceTypes)
            {
                var serviceBase = serviceType.GetInterfaces().Where(i => i.IsAppService()).FirstOrDefault();
                if (serviceBase is not null)
                {
                    services.AddScoped(serviceBase, serviceType);
                }
            }

            return services;
        }
    }

    #region Type Helpers

    internal static class TypeExtensions
    {
        internal static bool IsInstanceableAppService(this Type type) =>
            type.IsAppService()
            && type.IsClass
            && !type.IsAbstract
            && !type.IsGenericType;

        internal static bool IsAppService(this Type type) => type.IsAssignableTo(typeof(IService));
    }

    #endregion
}
