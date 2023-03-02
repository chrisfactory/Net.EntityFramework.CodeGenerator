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
            var builders = explorer.Get().ToList();
        }
        //public CodeGenerator(ISqlGOperationsProvider provider)
        //{
        //    var actions = provider.GetOperations().ToList();
        //    foreach (var action in actions)
        //    {
        //        action.ExecuteAsync(CancellationToken.None);
        //    } 
        //} 
    }
}
