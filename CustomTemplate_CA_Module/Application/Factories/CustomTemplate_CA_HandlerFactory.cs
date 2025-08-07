using Microsoft.Extensions.DependencyInjection;

namespace CustomTemplate_CA_Module.Application.Factories;

public class CustomTemplate_CA_HandlerFactory(IServiceProvider _serviceProvider)
{
    public THandler GetHandler<THandler>() where THandler : class
    {
        var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetService<THandler>() ?? throw new InvalidOperationException($"Handler for type {typeof(THandler).FullName} is not registered.");
        return handler;
    }
}
