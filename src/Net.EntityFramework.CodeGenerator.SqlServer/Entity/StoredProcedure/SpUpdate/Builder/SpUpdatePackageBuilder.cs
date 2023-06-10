using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpUpdatePackageBuilder<TEntity> : PackageBuilder, ISpUpdatePackageBuilder<TEntity>
         where TEntity : class
    {
        public SpUpdatePackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<ISpUpdateParametersProvider, SpUpdateParametersProvider>();
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SpUpdateNameProvider>();
            Services.AddSingleton<IStoredProcedureEfCallerNameProvider, SpUpdateEfCallerNameProvider>();
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SqlSpUpdatePackageContentProvider>();
            //intentBuilder.DefineIntentProvider<EfDbContextExtensionBuilderTarget, EfDbContextExtensionSpUpdatePackageContentProvider>();
        }
    }
}
