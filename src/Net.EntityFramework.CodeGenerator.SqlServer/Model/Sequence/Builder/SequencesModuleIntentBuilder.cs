using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesModuleIntentBuilder : PackageBuilder<ICreateSequenceSource, SequencesSource>, ISequencesModuleIntentBuilder
    {
        public SequencesModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }

        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<SequencesTarget, SequencesPackageContentProvider>();
        }
    }
}
