using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesModuleIntentBuilder : PackageModuleIntentBuilder<ICreateSequenceSource, SequencesSource>, ISequencesModuleIntentBuilder
    {
        public SequencesModuleIntentBuilder(IModuleStack moduleStack) : base(moduleStack)
        {
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<ICreateSequenceSource, SequencesTarget, SequencesPackageContentProvider>>();
        }

        public override IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }
}
