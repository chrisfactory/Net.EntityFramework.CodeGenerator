using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexPackageBuilder : PackageBuilder<ICreateIndexSource, CreateIndexSource>, ICreateIndexPackageBuilder
    {
        public CreateIndexPackageBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }

        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<DataProjectTarget, CreateIndexPackageContentProvider>();
        }
    }
}
