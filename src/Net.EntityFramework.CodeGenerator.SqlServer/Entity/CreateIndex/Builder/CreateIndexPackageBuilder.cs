using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexPackageBuilder : PackageBuilder, ICreateIndexPackageBuilder
    {
        public CreateIndexPackageBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, CreateIndexPackageContentProvider>();
        }
    }
}
