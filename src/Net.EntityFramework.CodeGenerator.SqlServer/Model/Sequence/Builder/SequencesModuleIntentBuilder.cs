using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesModuleIntentBuilder : ISequencesModuleIntentBuilder
    {
        public SequencesModuleIntentBuilder(IModuleStack moduleStack)
        {
            Services = moduleStack.BaseStack;
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
