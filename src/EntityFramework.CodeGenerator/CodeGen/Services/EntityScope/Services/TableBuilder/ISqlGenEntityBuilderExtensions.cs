using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class IStoredProceduresBuilderExtensions
    {
        public static ISqlGenEntityBuilder CreateTable(this ISqlGenEntityBuilder genBuilder, Action<ICreateTableBuilder>? builder = null)
        { 
            return genBuilder.AddGenActionBuilder<ICreateTableBuilder, CreateTableBuilder>(builder);
        }
    }




    public interface ICreateTableBuilder : IActionBuilder
    {
    }

    internal class CreateTableBuilder : ActionBuilder, ICreateTableBuilder
    {

        public override IActionProvider Build()
        { 
            Services.AddSingleton<ICreateTableOperationProvider, CreateTableOperationProvider>();
            Services.AddSingleton<IActionProvider, CreateTableSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }



}
