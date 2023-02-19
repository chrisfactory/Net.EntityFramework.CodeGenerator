using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public interface ISqlGenModelBuilder 
    {
        IServiceCollection Services { get; }
        ISqlGenActionBuilderProvider Build();
    }
    internal class SqlGenModelBuilder : ISqlGenModelBuilder 
    {
        public SqlGenModelBuilder()
        {
            Services = new ServiceCollection();
        }

        public IServiceCollection Services { get; }

        public ISqlGenActionBuilderProvider Build()
        {
            Services.AddSingleton<ISqlGenActionBuilderProvider, SqlGenActionBuilderProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionBuilderProvider>();
        }
    }
     
}
