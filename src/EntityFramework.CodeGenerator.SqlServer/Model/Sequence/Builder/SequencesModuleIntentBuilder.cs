using EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesModuleIntentBuilder : ISequencesModuleIntentBuilder
    {
        public SequencesModuleIntentBuilder(IServiceCollection stack)
        {
            Services = stack; 
            Services.AddSingleton<IPackageContentSource, SequencesSource>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<SequencesTarget, SequencesPackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
