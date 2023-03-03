using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class EntityModuleIntentBaseStackFactory : IEntityModuleIntentBaseStackFactory
    {
        private readonly INodeSnapshotPoint _ModuleBaseStack;
        public EntityModuleIntentBaseStackFactory(IServiceProvider provider, IDbContextModelExtractor model)
        {
            var services = provider.GetNode("entity.modules.base.stack").CreateBranch();
            services.AddSingleton(model);
            _ModuleBaseStack = services.CreateNode("module.intent.base.stack");
        }
        public IServiceCollection Create()
        {
            var services = _ModuleBaseStack.CreateBranch();
            services.AddSingleton<IPackageIntentFactory, PackageIntentFactory>();
            services.AddSingleton(p => p.GetRequiredService<IPackageIntentFactory>().Create());
            services.AddSingleton<IPackageModuleIntent, PackageModuleIntent>();
            return services;
        }
    }
}
