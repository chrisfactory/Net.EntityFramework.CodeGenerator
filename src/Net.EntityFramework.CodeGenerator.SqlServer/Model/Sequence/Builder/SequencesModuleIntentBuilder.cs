using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesModuleIntentBuilder : PackageBuilder, ISequencesModuleIntentBuilder
    {
        public SequencesModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SequencesPackageContentProvider>();
        }
    }
}
