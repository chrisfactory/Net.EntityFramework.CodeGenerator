using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static class ISqlGBuilderExtensions
    {
        public static ISqlGBuilder AddEntity<TEntity>(this ISqlGBuilder services, Action<IEntityStrategyBuilder> builder)
            => services.AddEntity<TEntity>((p, b) => builder(b));

        public static ISqlGBuilder AddEntity<TEntity>(this ISqlGBuilder services, Action<IServiceProvider, IEntityStrategyBuilder> builder)
        {
            services.Services.AddSingleton(p =>
            {
                var b = p.GetRequiredService<IEntityStrategyBuilder>();
                builder(p, b);
                return b.Build();
            });
            return services;
        }
    }
}
