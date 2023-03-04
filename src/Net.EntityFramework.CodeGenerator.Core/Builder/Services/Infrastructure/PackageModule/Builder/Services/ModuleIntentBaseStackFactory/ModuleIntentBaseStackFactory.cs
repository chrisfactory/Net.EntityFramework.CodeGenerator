using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class ModuleIntentBaseStackFactory<TPackageScope> : IModuleIntentBaseStackFactory
        where TPackageScope : class, IPackageScope
    {

        private readonly INodeSnapshotPoint _ModuleBaseStack;
        public ModuleIntentBaseStackFactory(IServiceProvider provider, IDbContextModelExtractor model)
        {
            var services = provider.GetNode(Constants.ModuleBaseStackKey).CreateBranch();
            services.AddSingleton(model);
            _ModuleBaseStack = services.CreateNode(Constants.ModuleIntentBaseStackKey);
        }
        public IServiceCollection Create()
        {
            var services = _ModuleBaseStack.CreateBranch();
            services.AddSingleton<IPackageScope, TPackageScope>();
            services.AddSingleton<IPackageIdentity, PackageIdentity>();
            services.AddSingleton<IPackageIntentFactory, PackageIntentFactory>();
            services.AddSingleton(p => p.GetRequiredService<IPackageIntentFactory>().Create());
            services.AddSingleton<IPackageModuleIntent, PackageModuleIntent>();
            return services;
        }
    }
}
