namespace ServiceProviderPoC
{
    using System;

    public class ServiceScope
    {
        public string? Id { get; init; }

        public required IServiceProvider ServiceProvider { get; init; }
    }
}
