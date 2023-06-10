using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpInsertPackageBuilder<TEntity> : PackageBuilder, ISpInsertPackageBuilder<TEntity>
         where TEntity : class
    {
        public SpInsertPackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<ISpInsertParametersProvider, SpInsertParametersProvider>();
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SpInsertNameProvider>();
            Services.AddSingleton<IStoredProcedureEfCallerNameProvider, SpInsertEfCallerNameProvider>();
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SqlSpInsertPackageContentProvider>();
            intentBuilder.DefineIntentProvider<EfDbContextExtensionBuilderTarget, EfDbContextExtensionSpInsertPackageContentProvider>();
        }
    }
}
