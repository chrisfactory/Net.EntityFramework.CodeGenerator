using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public abstract class PackageBuilder<TSource, TSourceImplementation> : IPackageBuilder
           where TSource : class, IPackageSource
           where TSourceImplementation : class, TSource
    {
        public PackageBuilder(IPackageStack packageStack)
        {
            Services = packageStack.GetNewStack<TSource, TSourceImplementation>();
        }
        public IServiceCollection Services { get; }

        protected abstract void DefineIntentProviders(IIntentsBuilder intentBuilder);
        public IPackage Build()
        {
            var intentNode = Services.CreateNode();

            var internalPackageServices = intentNode.CreateBranch();

            internalPackageServices.AddSingleton<IPackage, Package>();
            internalPackageServices.AddSingleton(p =>
            {
                var builder = new IntentsBuilder(intentNode);
                DefineIntentProviders(builder);
                return builder.Build();
            });
            return internalPackageServices.BuildServiceProvider().GetRequiredService<IPackage>();
        }
        private class IntentsBuilder : IIntentsBuilder
        {
            public IntentsBuilder(INodeSnapshotPoint node)
            {
                SnapshotPoint = node;
                Services = node.CreateBranch();
            }
            public IServiceCollection Services { get; }

            public INodeSnapshotPoint SnapshotPoint { get; }

            public IEnumerable<IIntent> Build()
            {
                return Services.BuildServiceProvider().GetRequiredService<IEnumerable<IIntent>>();
            }
        }
    }
}
