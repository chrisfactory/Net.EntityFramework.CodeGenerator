using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpDeletePackageBuilder<TEntity> : PackageBuilder, ISpDeletePackageBuilder<TEntity>
         where TEntity : class
    {
        public SpDeletePackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<ISpDeleteParametersProvider, SpDeleteParametersProvider>();
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SpDeleteNameProvider>();
            Services.AddSingleton<IStoredProcedureEfCallerNameProvider, SpDeleteEfCallerNameProvider>();
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SqlSpDeletePackageContentProvider>();
            intentBuilder.DefineIntentProvider<EfDbContextExtensionBuilderTarget, EfDbContextExtensionSpDeletePackageContentProvider>();
        }
    }
}
