using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class EntityModuleBuilder : IEntityModuleBuilder
    {
        public EntityModuleBuilder(IMutableEntityType metadata)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(metadata);
            Services.AddTransient<ICreateTableModuleIntentBuilder, CreateTableModuleIntentBuilder>();
        }
        public IServiceCollection Services { get; }

        public IPackageModuleIntentProvider Build()
        {
            Services.AddSingleton<IPackageModuleIntentProvider, PackageModuleIntentProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntentProvider>();
        }
    }
}
