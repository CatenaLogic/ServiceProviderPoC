namespace Catel
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public static class IServiceProviderExtensions
    {
        public static bool IsRegistered<TService>(this IServiceProvider serviceProvider)
        {
            return IsRegistered(serviceProvider, typeof(TService));
        }

        public static bool IsRegistered(this IServiceProvider serviceProvider, Type serviceType)
        {
            var serviceChecker = serviceProvider.GetRequiredService<IServiceProviderIsService>();

            return serviceChecker.IsService(serviceType);
        }

        public static IReadOnlyList<ServiceDescriptor> GetServiceDescriptors<T>(this IServiceProvider serviceProvider)
        {
            return GetServiceDescriptors(serviceProvider, typeof(T));
        }

        public static IReadOnlyList<ServiceDescriptor> GetServiceDescriptors(this IServiceProvider serviceProvider, Type type)
        {
            var serviceCollection = serviceProvider.GetServiceCollection();

            var serviceDescriptors = new List<ServiceDescriptor>();

            foreach (var service in serviceCollection)
            {
                if (service.ServiceType != type)
                {
                    continue;
                }

                serviceDescriptors.Add(service);
            }

            return serviceDescriptors;
        }

        public static IServiceCollection GetServiceCollection(this IServiceProvider serviceProvider, string key = "CatelServiceCollection")
        {
            var serviceCollection = serviceProvider.GetKeyedService<IServiceCollection>(key);
            if (serviceCollection is not null)
            {
                return serviceCollection;
            }

            return new ServiceCollection();
        }
    }
}
