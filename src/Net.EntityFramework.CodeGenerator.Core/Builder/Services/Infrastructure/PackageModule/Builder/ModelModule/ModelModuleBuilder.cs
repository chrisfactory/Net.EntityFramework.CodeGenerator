using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class ModelModuleBuilder : IModelModuleBuilder
    {
        private readonly Action<IModelModuleBuilder>? _configure;
        public ModelModuleBuilder(IMutableModel metadata, Action<IModelModuleBuilder> configure)
        {
            _configure = configure;

            Services = new ServiceCollection();
            Services.AddSingleton(metadata);
            Services.AddSingleton(PackageTokenProvider); 
        }
        public IServiceCollection Services { get; }
        public IPackageTokenProvider PackageTokenProvider { get; } = new PackageTokenProvider();

        public IModulePackage Build()
        {

            var packageNode = Services.CreateNode();
            Services.AddTransient<IPackageStack>(p => new PackageStack(packageNode));
            _configure?.Invoke(this);

            var finalNode = Services.CreateNode();

            var finalServices = finalNode.CreateBranch();

            finalServices.AddSingleton<IPackageModuleBuilder>(this);
            finalServices.AddSingleton<IModulePackage, Module>();
            var provider = finalServices.BuildServiceProvider();
            return provider.GetRequiredService<IModulePackage>(); 
        } 
    }
}
