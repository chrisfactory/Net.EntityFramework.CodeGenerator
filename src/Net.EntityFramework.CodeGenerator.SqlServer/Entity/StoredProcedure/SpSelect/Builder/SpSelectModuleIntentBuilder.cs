using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SpSelectModuleIntentBuilder : PackageModuleIntentBuilder<ISpSelectCodeGeneratorSource, SpSelectSource>, ISpSelectModuleIntentBuilder
    {
        public SpSelectModuleIntentBuilder(IModuleStack moduleStack) : base(moduleStack)
        {
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SelectTableProcedureNameProvider>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<ISpSelectCodeGeneratorSource, SqlSpSelectTarget, SqlSpSelectPackageContentProvider>>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<ISpSelectCodeGeneratorSource, DbServiceSpSelectTarget, DbServiceSpSelectPackageContentProvider>>();
        }
        public override IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
