using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class EntityModuleBuilder : IEntityModuleBuilder
    { 
        public EntityModuleBuilder(IMutableEntityType metadata)
        { 
            Services = CreateBaseNode(metadata).CreateBranch();
            Services.AddSingleton<IModuleIntentBaseStackFactory, ModuleIntentBaseStackFactory>();
            Services.AddTransient(p => p.GetRequiredService<IModuleIntentBaseStackFactory>().Create());


            Services.AddTransient<ICreateTableModuleIntentBuilder, CreateTableModuleIntentBuilder>();
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
