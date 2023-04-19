using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceModuleIntentBuilder : IDbServiceModuleIntentBuilder
    {
        public DbServiceModuleIntentBuilder(IModuleStack moduleStack)
        {
            Services = moduleStack.BaseStack; 
            Services.AddSingleton<IPackageContentSource, DbServiceSource>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<DbServiceTarget, DbServicePackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPostBuildPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPostBuildPackageModuleIntent>();
        }
    }
}
