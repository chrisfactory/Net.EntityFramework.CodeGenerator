using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesModuleIntentBuilder : ISequencesModuleIntentBuilder
    {
        public SequencesModuleIntentBuilder(IPackageStack packageStack)
        {
            Services = packageStack.GetNewStack<ICreateSequenceSource, SequencesSource>();
            Services.AddSingleton<ICreateSequenceSource, SequencesSource>();
        }
        //protected override void Prepare(IPackageIntentBuilderFactory preBuilder)
        //{
        //    preBuilder.DefineIntentBuilder<SequencesTarget, SequencesPackageContentProvider>();
        //}

        public IServiceCollection Services { get; }

        public IPackage Build()
        {
            return Services.BuildServiceProvider().GetRequiredService<IPackage>();
        }
    }
}
