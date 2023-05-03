using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IModule
    {
        IServiceProvider Provider { get; }
    }
    internal class Module : IModule
    {
        public Module(IServiceProvider provider)
        {
            Provider = provider;
        }
        public IServiceProvider Provider { get; }
    }
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
        public IModuleStack Create()
        {
            var services = _ModuleBaseStack.CreateBranch();
            services.AddSingleton<IModule, Module>();
            services.AddSingleton<IPackageScope, TPackageScope>();
            services.AddSingleton<IPackageIdentity, PackageIdentity>();
            services.AddSingleton<IPackageIntentFactory, PackageIntentFactory>();
            services.AddSingleton(p => p.GetRequiredService<IPackageIntentFactory>().Create());
            services.AddSingleton<IPostBuildPackageModuleIntent, PackageModuleIntent>();
            services.AddSingleton<IPackageModuleIntent>(p => p.GetRequiredService<IPostBuildPackageModuleIntent>());
            return new ModuleIntentBaseStack(services);
        }

        private class ModuleIntentBaseStack : IModuleStack
        {
            public ModuleIntentBaseStack(IServiceCollection services)
            {
                BaseStack = services;
            }
            public IServiceCollection BaseStack { get; }
        }
    }
}
