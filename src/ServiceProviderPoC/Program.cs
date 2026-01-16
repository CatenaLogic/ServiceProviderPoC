namespace ServiceProviderPoC
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ServiceProviderPoC.IoC;
    using ServiceProviderPoC.Processors;
    using ServiceProviderPoC.Services;

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddKeyedSingleton<IServiceCollection>("CatelServiceCollection", serviceCollection);

            serviceCollection.AddLogging(x =>
            {
                x.AddConsole();
                x.AddDebug();

                x.SetMinimumLevel(LogLevel.Debug);
            });

            serviceCollection.AddSingleton<IScopeServiceCollectionProvider, ScopeServiceCollectionProvider>();
            serviceCollection.AddSingleton<IServiceScopeManager, ServiceScopeManager>();
            serviceCollection.AddSingleton<TestProcessor>();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var processor = serviceProvider.GetRequiredService<TestProcessor>();

            await processor.ProcessAsync();
        }
    }
}
