using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
            Services.AddTransient<ICreateFileActionBuilder, CreateFileActionBuilder>();
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
            foreach (var item in provider.GetOperations())
            {
                item.ExecuteAsync(CancellationToken.None);
            }

        }

    }
}
