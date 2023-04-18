using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SpSelectModuleIntentBuilder : ISpSelectModuleIntentBuilder
    {
        public SpSelectModuleIntentBuilder(IModuleStack moduleStack)
        {
            Services = moduleStack.BaseStack; 
            Services.AddSingleton<IPackageContentSource, SpSelectSource>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<SqlSpSelectTarget, SqlSpSelectPackageContentProvider>>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<DbServiceSpSelectTarget, DbServiceSpSelectPackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
