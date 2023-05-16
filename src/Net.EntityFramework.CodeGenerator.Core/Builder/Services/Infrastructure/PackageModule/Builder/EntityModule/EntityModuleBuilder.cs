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
            finalServices.AddSingleton<IPackageModuleIntentProvider, PackageModuleIntentProvider>();
            finalServices.AddSingleton<IPackageModuleBuilder>(this);
            finalServices.AddSingleton<IModulePackage, Module>();
            var provider = finalServices.BuildServiceProvider();
            return provider.GetRequiredService<IModulePackage>();
        }
    }
    public interface IPackageStack
    {
        IServiceCollection GetNewStack<TSource, TSourceImplementation>()
            where TSource : class, IPackageSource
            where TSourceImplementation : class, TSource;
    }
    internal class PackageStack : IPackageStack
    {
        private readonly IServiceCollection _stack;
        public PackageStack(INodeSnapshotPoint moduleSnapshot)
        {
            _stack = moduleSnapshot.CreateBranch();
        }



        public IServiceCollection GetNewStack<TSource, TSourceImplementation>()
            where TSource : class, IPackageSource
            where TSourceImplementation : class, TSource
        {
            _stack.AddSingleton<TSource, TSourceImplementation>();
            _stack.AddSingleton<IPackageSource>(p => p.GetRequiredService<TSource>());
            return _stack;
        }
    }
}
