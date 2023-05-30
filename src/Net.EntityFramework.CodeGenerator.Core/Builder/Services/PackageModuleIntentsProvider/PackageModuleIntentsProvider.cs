using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleIntentsProvider : IPackageModuleIntentsProvider
    {
        private readonly IDbContextModelExtractor _context;
        private readonly IPackageModuleBuilderProvider _moduleBuilderProvider;
        public PackageModuleIntentsProvider(
            IDbContextModelExtractor context,
            IPackageModuleBuilderProvider moduleBuilderProvider)
        {
            _context = context;
            _moduleBuilderProvider = moduleBuilderProvider;
        }

        public IEnumerable<IPackage> Get()
        {
            var builders = _moduleBuilderProvider.Get().ToList();
             
            foreach (var moduleBuilder in builders)
                moduleBuilder.Services.AddSingleton(_context);


            foreach (IPackageModuleBuilder moduleBuilder in builders)
            {
                var module = moduleBuilder.Build();
                foreach (var package in module.Packages)
                    yield return package;
            }
        }
    }
}
