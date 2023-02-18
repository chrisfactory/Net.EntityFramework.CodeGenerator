using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class IStoredProceduresBuilderExtensions
    {
        public static ISqlGenEntityBuilder CreateTable(this ISqlGenEntityBuilder genBuilder, Action<ICreateTableBuilder>? builder = null)
        { 
            return genBuilder.AddGenActionBuilder<ICreateTableBuilder, CreateTableBuilder>(builder);
        }
    }




    public interface ICreateTableBuilder : ISqlGenActionBuilder
    {
    }

    internal class CreateTableBuilder : SqlGenActionBuilder, ICreateTableBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            Services.AddSingleton<ISqlFileInfoFactory, GetSqlFileInfoFactory>();
            Services.AddSingleton<ICreateTableOperationProvider, CreateTableOperationProvider>();
            Services.AddSingleton<ISqlGenActionProvider, CreateTableSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
        }
    }



}
