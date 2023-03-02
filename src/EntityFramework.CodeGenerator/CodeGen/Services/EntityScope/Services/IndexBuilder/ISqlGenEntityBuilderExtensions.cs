using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class IStoredProceduresBuilderExtensions
    {
        public static ISqlGenEntityBuilder CreateIndex(this ISqlGenEntityBuilder genBuilder, Action<ICreateIndexBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<ICreateIndexBuilder, CreateIndexBuilder>(builder);
        }
    }



    public interface ICreateIndexBuilder : IActionBuilder
    {
    }

    internal class CreateIndexBuilder : ActionBuilder, ICreateIndexBuilder
    {

        public override IActionProvider Build()
        { 
            Services.AddSingleton<ICreateIndexOperationsProvider, CreateIndexOperationsProvider>();
            Services.AddSingleton<IActionProvider, CreateIndexSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }




}
