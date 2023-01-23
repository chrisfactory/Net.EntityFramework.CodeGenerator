using Microsoft.Extensions.DependencyInjection;

namespace DataBaseAccess
{
    public interface IConnectionsStringProviderBuilder
    {
        IServiceCollection Services { get; }
        IConnectionsStringProvider Build();
    }
}
