using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
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
            Services.AddSingleton<IDbContextModelExtractor, DbContextModelExtractor>();
            Services.AddSingleton<ISqlGOperationsProvider, SqlGOperationsProvider>();
            Services.AddSingleton<ICodeGenerator, CodeGenerator>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ICodeGenerator>();
        }
    }


    public interface ICodeGenerator
    {

    }

    internal class CodeGenerator : ICodeGenerator
    {
        public CodeGenerator(ISqlGOperationsProvider provider)
        {
            var actions = provider.GetOperations().ToList();
            foreach (var action in actions)
            {
                action.ExecuteAsync(CancellationToken.None);
            } 
        } 
    }
}
