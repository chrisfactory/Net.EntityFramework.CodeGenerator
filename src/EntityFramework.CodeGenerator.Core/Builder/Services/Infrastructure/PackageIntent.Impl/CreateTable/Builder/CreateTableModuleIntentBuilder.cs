using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class CreateTableModuleIntentBuilder : ICreateTableModuleIntentBuilder
    {
        public CreateTableModuleIntentBuilder(IMutableEntityType metadata)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(metadata);
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<TablePackageScope, CreateTableSource, TableTarget>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            Services.AddSingleton<IPackageIntentFactory, PackageIntentFactory>();
            Services.AddSingleton(p => p.GetRequiredService<IPackageIntentFactory>().Create());
            Services.AddSingleton<IPackageModuleIntent, PackageModuleIntent>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    } 
}
