# Service Provider Proof of Concept (PoC)

This library is a PoC to see if dynamic (child) service providers can be used.

## Requirements

When using the dependency injection extensions from Microsoft, all the services has to be known upfront. This is a great way to build up an application.

Some applications require dynamic service registrations where multiple services with a different key (scope) must run side-by-side. Although the service collection supports keyed registrations, the keys must be known upfront. A few use cases where this is not possible are:

* Workspaces
* Filters
* Multi document interface (MDI) 

Instead of having to manage keyed state in every service, this PoC supports creating (and disposing) runtime service collections with a corresponding service provider. 

## Implementation

### Registering a scope

To register a new scope, the application must use  `IServiceScopeManager.AddScope(ScopeContext context)`:

```
var scope = _scopeManager.AddScope(new ServiceScopeContext
{
    Id = "TestScope",
    DisposeScopeServices = true,
});
```

### Registering services for a scope

To register services for a dynamic / runtime scope, the application must register one or more types implementing `IScopeServiceCollectionProvider`.

Example implementation:

```
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
```

Then the class must be registered during the program registrations:

```
serviceCollection.AddSingleton<IScopeServiceCollectionProvider, ScopeServiceCollectionProvider>();
```

If `ScopeContext.IsIsolated` is **not** set the `true`, all services registered in the parent `IServiceProvider` will automatically be added to the child service collection as well.

### Using the scope

To use runtime scopes, use the following code:

```
var scope = scopeManager.GetScope("TestScope");
if (scope is not null)
{
    var scopedService = scope.ServiceProvider.GetRequiredService<IFilterServce>();
}
```

Types can also be constructed using `ActivatorUtilities`:

```
var scope = scopeManager.GetScope("TestScope");
if (scope is not null)
{
    var scopedObject = ActivatorUtilities.CreateInstance<MyObject>(scope.ServiceProvider);
}

```

### Removing a scope

To remove a scope, call `IServiceScopeManager.RemoveScope(string id)`. 

This will remove the scope and, optionally, dispose the explicitly registered services or service provider.