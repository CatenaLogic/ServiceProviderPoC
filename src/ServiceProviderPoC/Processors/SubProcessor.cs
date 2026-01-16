namespace ServiceProviderPoC.Processors
{
    using Microsoft.Extensions.Logging;
    using ServiceProviderPoC.Services;

    public class SubProcessor : IProcessor
    {
        private readonly ILogger<SubProcessor> _logger;
        private readonly IFilterService _filterService;

        public SubProcessor(ILogger<SubProcessor> logger, IFilterService filterService)
        {
            _logger = logger;
            _filterService = filterService;
        }

        public async Task ProcessAsync()
        {
            _logger.LogInformation("Sub processing");
        }
    }
}
