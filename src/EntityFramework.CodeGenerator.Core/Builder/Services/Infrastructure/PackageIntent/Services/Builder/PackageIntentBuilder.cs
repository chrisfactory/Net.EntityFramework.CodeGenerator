using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class PackageIntentBuilder<TScope, TContentSource, TTarget> : IPackageIntentBuilder
        where TScope : class, IPackageScope
        where TContentSource : class, IPackageContentSource
        where TTarget : class, IPackageTarget
    {
        public PackageIntentBuilder(IMutableEntityType metadata)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(metadata);
            Services.AddSingleton<IPackageScope, TScope>();
            Services.AddSingleton<IPackageContentSource, TContentSource>();
            Services.AddSingleton<IPackageTarget, TTarget>();

            Services.AddSingleton<IPackageIdentity, PackageIdentity>();
        }
        public IServiceCollection Services { get; }

        public IPackageIntent Build()
        {

            Services.AddSingleton<IPackageIntent, PackageIntent>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageIntent>();
        }
    }
}
