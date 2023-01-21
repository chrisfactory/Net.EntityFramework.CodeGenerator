using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal class SqlGBuilder : ISqlGBuilder
    {
        public SqlGBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public ISqlGenerator Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenerator>();
        }
    }
}
