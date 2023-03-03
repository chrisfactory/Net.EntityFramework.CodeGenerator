using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class EntityModuleBuilder : IEntityModuleBuilder
    { 
        public EntityModuleBuilder(IMutableEntityType metadata)
        { 
            Services = CreateBaseNode(metadata).CreateBranch();
            Services.AddSingleton<IEntityModuleIntentBaseStackFactory, EntityModuleIntentBaseStackFactory>();
            Services.AddTransient(p => p.GetRequiredService<IEntityModuleIntentBaseStackFactory>().Create()); 
        
        }
        public IServiceCollection Services { get; }

        private INodeSnapshotPoint CreateBaseNode(IMutableEntityType metadata)
        {
            var services = new ServiceCollection();
            services.AddSingleton(metadata);
            return services.CreateNode("entity.modules.base.stack");
        }


        public IPackageModuleIntentProvider Build()
        {
            Services.AddSingleton<IPackageModuleIntentProvider, PackageModuleIntentProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntentProvider>();
        }
    }
}
