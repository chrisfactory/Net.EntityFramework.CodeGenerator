using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaModuleIntentBuilder : PackageModuleIntentBuilder<IEnsureSchemaSource, EnsureSchemaSource>, IEnsureSchemaModuleIntentBuilder
    {
        public EnsureSchemaModuleIntentBuilder(IModuleStack moduleStack) : base(moduleStack)
        {
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<IEnsureSchemaSource, EnsureSchemaTarget, EnsureSchemaPackageContentProvider>>();
        }

        public override IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
