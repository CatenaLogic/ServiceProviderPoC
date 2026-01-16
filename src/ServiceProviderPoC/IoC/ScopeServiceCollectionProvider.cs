namespace ServiceProviderPoC.IoC
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using ServiceProviderPoC.Services;

    internal class ScopeServiceCollectionProvider : IScopeServiceCollectionProvider
    {
        private readonly ILogger<ScopeServiceCollectionProvider> _logger;

        public ScopeServiceCollectionProvider(ILogger<ScopeServiceCollectionProvider> logger)
        {
            _logger = logger;
        }

        public IServiceCollection AddServices(IServiceCollection serviceCollection, string? scopeId)
        {
            _logger.LogInformation("Adding services for scope '{ScopeId}'", scopeId);

            serviceCollection.TryAddSingleton<IFilterService, FilterService>();

            return serviceCollection;
        }
    }
}
