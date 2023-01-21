using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal class SqlGBuilder : ISqlGBuilder
    {
        public SqlGBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<IProjectResolver, CurrentCSProjectResolver>();
        }
        public IServiceCollection Services { get; }

        public ICodeGenerator Build()
        {
            Services.AddTransient<IEntityStrategyBuilder, EntityStrategyBuilder>();
            Services.AddSingleton<ICodeGenerator, CodeGenerator>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ICodeGenerator>();
        }
    }
}
