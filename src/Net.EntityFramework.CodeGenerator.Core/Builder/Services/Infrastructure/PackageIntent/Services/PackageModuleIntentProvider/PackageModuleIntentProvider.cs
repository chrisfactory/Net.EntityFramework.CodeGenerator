using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleIntentProvider : IPackageModuleIntentProvider
    {
        private readonly IServiceProvider _provider;
        public PackageModuleIntentProvider(IServiceProvider provider)
        {
            _provider = provider;
        }


        public IEnumerable<IPackage> Get()
        {
            var packages = _provider.GetServices<IPackage>().ToList();
            var postBuilders = _provider.GetServices<IBuilder<IPostBuildPackage>>().ToList();
         
            foreach (var postBuilder in postBuilders) 
                foreach (var package in packages) 
                    postBuilder.Services.AddSingleton(package);  
            foreach (var postBuilder in postBuilders) 
                packages.Add(postBuilder.Build());
            return packages;
        }
    }
}
