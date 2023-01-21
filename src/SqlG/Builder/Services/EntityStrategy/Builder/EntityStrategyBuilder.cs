using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal class EntityStrategyBuilder : IEntityStrategyBuilder
    {
        public EntityStrategyBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }
        public IEntityStrategy Build()
        {
            Services.AddSingleton<IEntitySchemaFactory, EntitySchemaFactory>();
            Services.AddSingleton<IEntitySchemaProvider, EntitySchemaProvider>();
            Services.AddSingleton<IEntityStrategy, EntityStrategy>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IEntityStrategy>();
        }
    }
}
