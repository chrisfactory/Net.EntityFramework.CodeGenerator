using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceModuleIntentBuilder : PackageBuilder, IDbServiceModuleIntentBuilder
    {
        public DbServiceModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {

        }
        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DotNetProjectTarget, DbServicePackageContentProvider>();
        }
    }
}
