using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class EntityModuleBuilder : IEntityModuleBuilder
    {
        private readonly Action<IEntityModuleBuilder>? _configure;
        public EntityModuleBuilder(IMutableEntityType metadata, Action<IEntityModuleBuilder>? configure)
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
    public interface IPackageStack
    {
        IServiceCollection GetStack();
    }
    internal class PackageStack : IPackageStack
    {
        private readonly INodeSnapshotPoint _moduleSnapshot;
        public PackageStack(INodeSnapshotPoint moduleSnapshot) => _moduleSnapshot = moduleSnapshot; 
        public IServiceCollection GetStack() => _moduleSnapshot.CreateBranch();
    }
}
