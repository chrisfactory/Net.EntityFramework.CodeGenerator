using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceModuleIntentBuilder : PackageModuleIntentBuilder<IDbServiceCodeGeneratorSource, DbServiceSource>, IDbServiceModuleIntentBuilder
    {
        public DbServiceModuleIntentBuilder(IModuleStack moduleStack) : base(moduleStack)
        {
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<IDbServiceCodeGeneratorSource, DbServiceTarget, DbServicePackageContentProvider>>();
        }
        public override IPostBuildPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPostBuildPackageModuleIntent>();
        }
    }
}
