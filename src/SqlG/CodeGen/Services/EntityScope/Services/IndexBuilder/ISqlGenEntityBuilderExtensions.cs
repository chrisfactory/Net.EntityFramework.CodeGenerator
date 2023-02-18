using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class IStoredProceduresBuilderExtensions
    {
        public static ISqlGenEntityBuilder CreateIndex(this ISqlGenEntityBuilder genBuilder, Action<ICreateIndexBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<ICreateIndexBuilder, CreateIndexBuilder>(builder);
        }
    }



    public interface ICreateIndexBuilder : ISqlGenActionBuilder
    {
    }

    internal class CreateIndexBuilder : SqlGenActionBuilder, ICreateIndexBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            Services.AddSingleton<ISqlFileInfoFactory, GetSqlFileInfoFactory>();
            Services.AddSingleton<ICreateIndexOperationsProvider, CreateIndexOperationsProvider>();
            Services.AddSingleton<ISqlGenActionProvider, CreateIndexSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
        }
    }




}
