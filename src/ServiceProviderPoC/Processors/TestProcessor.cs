namespace ServiceProviderPoC.Processors
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ServiceProviderPoC.Services;

    internal class TestProcessor : IProcessor
    {
        private readonly ILogger<TestProcessor> _logger;
        private readonly IServiceScopeManager _scopeManager;

        public TestProcessor(ILogger<TestProcessor> logger, IServiceScopeManager scopeManager)
        {
            _logger = logger;
            _scopeManager = scopeManager;
        }

        public async Task ProcessAsync()
        {
            _logger.LogInformation("Processing");

            var scope1 = _scopeManager.AddScope(new ServiceScopeContext
            {
                Id = "TestScope",
                DisposeScopeServices = true,
            });

            // 2nd time should return same scope
            var scope2 = _scopeManager.AddScope(new ServiceScopeContext
            {
                Id = "TestScope"
            });

            var filterService = scope1.ServiceProvider.GetRequiredService<IFilterService>();

            await filterService.RunAsync();

            _scopeManager.RemoveScope("TestScope");
        }
    }
}
