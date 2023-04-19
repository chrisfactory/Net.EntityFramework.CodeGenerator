using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class EntityModuleBuilder : IEntityModuleBuilder
    {

        public EntityModuleBuilder(IMutableEntityType metadata)
        {
            Services = CreateBaseNode(metadata).CreateBranch();
            Services.AddSingleton<IModuleIntentBaseStackFactory, ModuleIntentBaseStackFactory<TablePackageScope>>();
            Services.AddTransient(p => p.GetRequiredService<IModuleIntentBaseStackFactory>().Create());

        }
        public IServiceCollection Services { get; } 
        public IPackageTokenProvider PackageTokenProvider { get; } = new PackageTokenProvider();

        private INodeSnapshotPoint CreateBaseNode(IMutableEntityType metadata)
        {
            var services = new ServiceCollection();
            services.AddSingleton(metadata);
            return services.CreateNode(Constants.ModuleBaseStackKey);
        }


        public IPackageModuleIntentProvider Build()
        {
            Services.AddSingleton<IPackageModuleIntentProvider, PackageModuleIntentProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntentProvider>();
        }
    }
}
