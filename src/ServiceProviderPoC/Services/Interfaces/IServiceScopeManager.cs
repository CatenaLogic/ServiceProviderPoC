namespace ServiceProviderPoC.Services
{
    using ServiceProviderPoC;

    public interface IServiceScopeManager
    {
        ServiceScope AddScope(ServiceScopeContext scopeContext);
        ServiceScope? GetScope(string id);
        bool RemoveScope(string id);
    }
}
