using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleProvider : IPackageModuleProvider
    {
        private readonly IServiceProvider _provider;
        public PackageModuleProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IEnumerable<IIntent> Get()
        {
            return _provider.GetServices<IIntent>();
        }
    }
}
