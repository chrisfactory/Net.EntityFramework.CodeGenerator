using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceModuleIntentBuilder : PackageBuilder<IDbServiceCodeGeneratorSource, DbServiceSource>, IDbServiceModuleIntentBuilder
    {
        public DbServiceModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {

        }
        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<DbServiceTarget, DbServicePackageContentProvider>();
        }
    }
}
