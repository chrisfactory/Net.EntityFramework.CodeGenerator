using Microsoft.Extensions.DependencyInjection;

namespace DataBaseAccess
{
    internal class ConnectionsStringProviderBuilder : IConnectionsStringProviderBuilder
    {
        public ConnectionsStringProviderBuilder()
        {
            Services = new ServiceCollection();
        }

        public IServiceCollection Services { get; }
        public IConnectionsStringProvider Build()
        {
            Services.AddSingleton<IConnectionsStringProvider, ConnectionsStringProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IConnectionsStringProvider>();
        }
    }
}
