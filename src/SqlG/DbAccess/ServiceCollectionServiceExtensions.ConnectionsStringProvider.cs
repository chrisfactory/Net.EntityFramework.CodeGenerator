using DataBaseAccess;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddConnectionsStringProvider(this IServiceCollection services, Action<IConnectionsStringProviderBuilder> configur)
        {
            services.TryAddSingleton<IConnectionsStringProviderBuilder, ConnectionsStringProviderBuilder>();

            services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IConnectionsStringProviderBuilder>();
                configur(builder);
                return builder.Build();
            });
            return services;
        }

        public static IConnectionsStringProviderBuilder AddConnectionsString(this IConnectionsStringProviderBuilder builder, string name, string cnx)
        {
            builder.Services.AddSingleton<IConnectionsStringDescriptor>(new FixedConnectionsStringDescriptor(name, cnx));
            return builder;
        }
    }
}
