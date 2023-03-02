using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder Tables(this ISqlGenModelBuilder genBuilder)
        {
            return genBuilder.AddGenActionBuilder<ICreateTablesBuilder, CreateTablesBuilder>();
        }
    }


    public interface ICreateTablesBuilder : IActionBuilder
    {
    }

    internal class CreateTablesBuilder : ActionBuilder, ICreateTablesBuilder
    {

        public override IActionProvider Build()
        {
            Services.AddTransient<ICreateTableBuilder, CreateTableBuilder>();
            Services.AddSingleton<IActionProvider, CreateTablesSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }


    public interface ICreateTablesSqlGenActionProvider : IActionProvider
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
        public IEnumerable<IAction> Get()
        {
            return new List<IAction>();
        }

        public ICreateTableBuilder GetTableBuilder()
        {
           return _provider.GetRequiredService<ICreateTableBuilder>();   
        }
    }
}
