using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder AllTables(this ISqlGenModelBuilder genBuilder)
        {
            return genBuilder.AddGenActionBuilder<ICreateTablesBuilder, CreateTablesBuilder>();
        }
    }


    public interface ICreateTablesBuilder : ISqlGenActionBuilder
    {
    }

    internal class CreateTablesBuilder : SqlGenActionBuilder, ICreateTablesBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            Services.AddTransient<ICreateTableBuilder, CreateTableBuilder>();
            Services.AddSingleton<ISqlGenActionProvider, CreateTablesSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
        }
    }


    public interface ICreateTablesSqlGenActionProvider : ISqlGenActionProvider
    {
        ICreateTableBuilder GetTableBuilder();
    }
    internal class CreateTablesSqlGenActionProvider : ICreateTablesSqlGenActionProvider
    {
        private readonly IServiceProvider _provider;
        public CreateTablesSqlGenActionProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IEnumerable<ISqlGenAction> Get()
        {
            return new List<ISqlGenAction>();
        }

        public ICreateTableBuilder GetTableBuilder()
        {
           return _provider.GetRequiredService<ICreateTableBuilder>();   
        }
    }
}
