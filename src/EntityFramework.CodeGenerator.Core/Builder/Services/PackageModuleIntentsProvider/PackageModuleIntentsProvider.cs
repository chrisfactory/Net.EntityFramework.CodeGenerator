using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleIntentsProvider : IPackageModuleIntentsProvider
    {
        private readonly IDbContextModelExtractor _ModelExtractor;
        private readonly IPackageModuleBuilderProvider _moduleBuilderProvider;
        public PackageModuleIntentsProvider(IDbContextModelExtractor model, IPackageModuleBuilderProvider moduleBuilderProvider)
        {
            _ModelExtractor = model;
            _moduleBuilderProvider = moduleBuilderProvider;
        }

        public IEnumerable<IPackageModuleIntent> Get()
        {
            foreach (var moduleBuilder in _moduleBuilderProvider.Get())
            {
                moduleBuilder.Services.AddSingleton(_ModelExtractor);
                var moduleProvider = moduleBuilder.Build();
                foreach (var module in moduleProvider.Get())
                    yield return module;
            }
        }
    }
}
