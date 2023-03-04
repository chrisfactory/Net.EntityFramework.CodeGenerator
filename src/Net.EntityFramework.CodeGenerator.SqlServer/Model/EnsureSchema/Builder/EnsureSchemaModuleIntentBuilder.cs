using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaModuleIntentBuilder : IEnsureSchemaModuleIntentBuilder
    {
        public EnsureSchemaModuleIntentBuilder(IServiceCollection stack)
        {
            Services = stack; 
            Services.AddSingleton<IPackageContentSource, EnsureSchemaSource>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<EnsureSchemaTarget, EnsureSchemaPackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
