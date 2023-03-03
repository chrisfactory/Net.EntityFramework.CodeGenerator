using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    public class PackageIntentBuilder<TContentSource, TTarget, TContentProvider> : IPackageIntentBuilder
        where TContentSource : class, IPackageContentSource
        where TTarget : class, IPackageTarget
        where TContentProvider : class, IPackageContentProvider
    {
        public PackageIntentBuilder(IPackageScope scope, IMutableEntityType metadata, IDbContextModelExtractor model)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(metadata);
            Services.AddSingleton(model);
            Services.AddSingleton(scope);
            Services.AddSingleton<IPackageContentSource, TContentSource>();
            Services.AddSingleton<IPackageTarget, TTarget>();
            Services.AddSingleton<IPackageContentProvider, TContentProvider>();
            Services.AddSingleton(p => p.GetRequiredService<IPackageContentProvider>().Get());
            Services.AddSingleton<IPackageIdentity, PackageIdentity>();
            Services.AddSingleton<IPackageIntent, PackageIntent>();
        }
        public IServiceCollection Services { get; }

        public IPackageIntent Build()
        {

          

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageIntent>();
        }
    }
}
