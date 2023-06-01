using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpSelectPackageBuilder : PackageBuilder<ISpSelectCodeGeneratorSource, SpSelectSource>, ISpSelectPackageBuilder
    {
        public SpSelectPackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SelectTableProcedureNameProvider>();
        }

        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<DataProjectTarget, SqlSpSelectPackageContentProvider>();
            intentBuilder.DefineIntent<DbServiceSpSelectTarget, DbServiceSpSelectPackageContentProvider>();
        }
    }
}
