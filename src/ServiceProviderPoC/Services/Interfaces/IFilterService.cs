namespace ServiceProviderPoC.Services
{
    using System;

    public interface IFilterService : IDisposable
    {
        Task RunAsync();
    }
}
