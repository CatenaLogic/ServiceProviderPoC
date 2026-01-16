namespace ServiceProviderPoC.Services
{
    using Microsoft.Extensions.Logging;

    public sealed class FilterService : IFilterService
    {
        private readonly ILogger<FilterService> _logger;

        public FilterService(ILogger<FilterService> logger)
        {
            _logger = logger;
        }

        public async Task RunAsync()
        {
            // Simulate some asynchronous work
            await Task.Delay(1000);

            _logger.LogInformation("FilterService is running.");
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing");
        }
    }
}
