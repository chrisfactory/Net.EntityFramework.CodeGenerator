using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTableModuleIntentBuilder : ICreateTableModuleIntentBuilder
    {
        public CreateTableModuleIntentBuilder(IServiceCollection stack)
        {
            Services = stack; 
            Services.AddSingleton<IPackageContentSource, CreateTableSource>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<TableTarget, CreateTablePackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
