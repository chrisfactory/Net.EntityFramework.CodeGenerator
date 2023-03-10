using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexModuleIntentBuilder : ICreateIndexModuleIntentBuilder
    {
        public CreateIndexModuleIntentBuilder(IServiceCollection stack)
        {
            Services = stack;
            Services.AddSingleton<IPackageContentSource, CreateIndexSource>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<IndexTarget, CreateIndexPackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
