using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SqlG
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseSqlGContext(this IServiceCollection services, Action<ISqlGBuilder> builder)
            => services.UseSqlGContext((p, b) => builder(b));

        public static IServiceCollection UseSqlGContext(this IServiceCollection services, Action<IServiceProvider, ISqlGBuilder> builder)
        {
            services.TryAddTransient<ISqlGBuilder, SqlGBuilder>();
            services.AddSingleton(p =>
            {
                var b = p.GetRequiredService<ISqlGBuilder>();
                builder(p, b);
                return b.Build();
            });
            return services;
        }
    }
}
