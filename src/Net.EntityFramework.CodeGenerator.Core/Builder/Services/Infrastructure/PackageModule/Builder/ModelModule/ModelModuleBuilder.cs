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
            Services.AddSingleton<IPackageModuleBuilder>(this);
            Services.AddSingleton<IModulePackage, Module>(); 
        }
        public IServiceCollection Services { get; }
        public IPackageTokenProvider PackageTokenProvider { get; } = new PackageTokenProvider();

        public IModulePackage Build()
        {
            var packageNode = Services.CreateNode();
            Services.AddTransient<IPackageStack>(p => new PackageStack(packageNode));
            _configure?.Invoke(this);

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IModulePackage>();
        }


    }
}
