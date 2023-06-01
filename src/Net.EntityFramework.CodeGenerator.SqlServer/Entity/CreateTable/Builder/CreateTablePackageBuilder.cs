using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageBuilder : PackageBuilder<ICreateTableSource, CreateTableSource>, ICreateTablePackageBuilder
    {
        public CreateTablePackageBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }
        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<DataProjectTarget, CreateTablePackageContentProvider>();
        }
    }
}
