using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class ModelModuleBuilder : IModelModuleBuilder
    { 
        public ModelModuleBuilder(IMutableModel metadata)
        { 
            Services = CreateBaseNode(metadata).CreateBranch();
            Services.AddSingleton<IModuleIntentBaseStackFactory, ModuleIntentBaseStackFactory<ModelPackageScope>>();
            Services.AddTransient(p => p.GetRequiredService<IModuleIntentBaseStackFactory>().Create());

        }
        public IServiceCollection Services { get; }

        private INodeSnapshotPoint CreateBaseNode(IMutableModel metadata)
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
