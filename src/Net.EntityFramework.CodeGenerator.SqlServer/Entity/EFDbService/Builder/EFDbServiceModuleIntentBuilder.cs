using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EFDbServiceModuleIntentBuilder : PackageBuilder<IDbServiceCodeGeneratorSource, DbServiceSource>, IEFDbServiceModuleIntentBuilder
    {
        public EFDbServiceModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {

        }
        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<DotNetProjectTarget, DbServicePackageContentProvider>();
        }
    }
}
