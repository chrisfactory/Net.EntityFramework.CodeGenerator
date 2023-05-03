using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTableModuleIntentBuilder : PackageModuleIntentBuilder<ICreateTableSource, CreateTableSource>, ICreateTableModuleIntentBuilder
    {
        public CreateTableModuleIntentBuilder(IModuleStack moduleStack) : base(moduleStack)
        {
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<ICreateTableSource, TableTarget, CreateTablePackageContentProvider>>();
        }

        public override IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
