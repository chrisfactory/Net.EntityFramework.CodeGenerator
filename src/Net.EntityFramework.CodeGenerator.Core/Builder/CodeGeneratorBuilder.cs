using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class CodeGeneratorBuilder : ICodeGeneratorBuilder
    {
        public CodeGeneratorBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public ICodeGenerator Build()
        {
            Services.AddSingleton<IDbContextModelExtractor, DbContextModelExtractor>();
            Services.AddSingleton<IPackageModuleBuilderProvider, PackageModuleBuilderProvider>();
            Services.AddSingleton<IPackageModuleIntentsProvider, PackageModuleIntentsProvider>();
            Services.AddSingleton<IIntentDispatcher, IntentDispatcher>();
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
        public CodeGenerator(IIntentDispatcher dispatcher)
        {
            dispatcher.DispatchAsync().GetAwaiter().GetResult();
        }
    }
}
