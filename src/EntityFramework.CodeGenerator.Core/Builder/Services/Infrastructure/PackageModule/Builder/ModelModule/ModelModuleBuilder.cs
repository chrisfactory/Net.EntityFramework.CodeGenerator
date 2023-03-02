using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class ModelModuleBuilder : IEntityModuleBuilder
    {
        public ModelModuleBuilder()
        {
            Services = new ServiceCollection();
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
