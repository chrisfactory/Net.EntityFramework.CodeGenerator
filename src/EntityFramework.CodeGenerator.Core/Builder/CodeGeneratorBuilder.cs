using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class CodeGeneratorBuilder : ICodeGeneratorBuilder
    {
        public CodeGeneratorBuilder()
        {
            Services = new ServiceCollection();
            //Services.AddSingleton<IProjectResolver, CurrentCSProjectResolver>();
        }
        public IServiceCollection Services { get; }

        public ICodeGenerator Build()
        {
            Services.AddSingleton<IDbContextModelExtractor, DbContextModelExtractor>();
            Services.AddSingleton<IPackageModuleBuilderProvider, PackageModuleBuilderProvider>();
            Services.AddSingleton<IPackageModuleIntentsProvider, PackageModuleIntentsProvider>();
            Services.AddSingleton<ICodeGenerator, CodeGenerator>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ICodeGenerator>();
        }
    }


    public interface ICodeGenerator
    {

    }

    internal class CodeGenerator : ICodeGenerator
    {
        public CodeGenerator(IDbContextModelExtractor ext, IPackageModuleIntentsProvider explorer)
        {
            var modules = explorer.Get().ToList();
            foreach (var module in modules)
            {
                var intents = module.Intents.ToList();
            }
        }
    }
}
