using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleIntentProvider : IPackageModuleIntentProvider
    {
        private readonly IServiceProvider _provider;
        public PackageModuleIntentProvider(IServiceProvider provider)
        {
            _provider = provider;
        }


        public IEnumerable<IPackageModuleIntent> Get()
        {
            return _provider.GetServices<IPackageModuleIntent>();
        }
    }
}
