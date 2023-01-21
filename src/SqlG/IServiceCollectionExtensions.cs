using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SqlG
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseSqlG(this IServiceCollection services, Action<ISqlGBuilder> builder)
            => services.UseSqlG((p, b) => builder(b));

        public static IServiceCollection UseSqlG(this IServiceCollection services, Action<IServiceProvider, ISqlGBuilder> builder)
        {
            services.TryAddSingleton<ISqlGBuilder, SqlGBuilder>();
            services.TryAddTransient<IEntityStrategyBuilder, EntityStrategyBuilder>();
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
