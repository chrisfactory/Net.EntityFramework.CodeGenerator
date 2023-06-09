using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{ 
    public class SpSelectPackageBuilder<TEntity> : PackageBuilder, ISpSelectPackageBuilder<TEntity>
         where TEntity : class
    {
        public SpSelectPackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<ISpSelectParametersProvider, SpSelectPrimaryKeysParameters>();
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SpSelectNameProvider>();
            Services.AddSingleton<IStoredProcedureEfCallerNameProvider, SpSelectEfCallerNameProvider>();
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SqlSpSelectPackageContentProvider>();
            intentBuilder.DefineIntentProvider<DbServiceBuilderTarget, DbServiceSpSelectPackageContentProvider>();
            intentBuilder.DefineIntentProvider<EfDbContextExtensionBuilderTarget, EfDbContextExtensionSpSelectPackageContentProvider>();
        }
    }
}
