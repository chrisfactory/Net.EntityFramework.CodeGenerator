using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpSelectPackageBuilder : PackageBuilder, ISpSelectPackageBuilder
    {
        public SpSelectPackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            //Services.AddSingleton<IStoredProcedureNameProvider, StoredProcedureNameProvider>();
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SqlSpSelectPackageContentProvider>();
            intentBuilder.DefineIntentProvider<DbServiceBuilderTarget, DbServiceSpSelectPackageContentProvider>();
            intentBuilder.DefineIntentProvider<EfDbContextExtensionBuilderTarget, EfDbContextExtensionSpSelectPackageContentProvider>();
        }
    }
}
