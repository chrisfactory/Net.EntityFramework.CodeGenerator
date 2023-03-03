using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EntityFramework.CodeGenerator.Core
{
    public class PackageIntentBuilder<TTarget, TContentProvider> : IPackageIntentBuilder
        where TTarget : class, IPackageTarget
        where TContentProvider : class, IPackageContentProvider
    {
        public PackageIntentBuilder(IPackageContentSource source, IPackageScope scope, IMutableEntityType metadata, IDbContextModelExtractor model)
        {
            var services = new ServiceCollection();
            services.AddSingleton(metadata);
            services.AddSingleton(model);
            services.AddSingleton(source);
            services.AddSingleton(scope);
            services.AddSingleton<IPackageTarget, TTarget>();
            services.AddSingleton<IPackageIdentity, PackageIdentity>();

            var intentBaseNode = services.CreateNode();

            Services = intentBaseNode.CreateBranch();


            Services.AddSingleton<IPackageContentProvider, TContentProvider>();
            Services.AddSingleton(p =>
            {
                var intents = new List<IPackageIntent>();
                foreach (var content in p.GetRequiredService<IPackageContentProvider>().Get())
                {
                    var intententServices = intentBaseNode.CreateBranch();
                    intententServices.AddSingleton(content);
                    intententServices.AddSingleton<IPackageIntent, PackageIntent>();
                    intents.Add(intententServices.BuildServiceProvider().GetRequiredService<IPackageIntent>());
                }
                return (IEnumerable<IPackageIntent>)intents;
            });

        }
        public IServiceCollection Services { get; }

        public IEnumerable<IPackageIntent> Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IEnumerable<IPackageIntent>>();
        }
    }
}
