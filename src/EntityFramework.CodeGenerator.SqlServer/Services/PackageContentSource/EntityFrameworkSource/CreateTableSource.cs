using EntityFramework.CodeGenerator.Core;

namespace EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTableSource : ICreateTableSource
    {
        public CreateTableSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Create Table";
        public IPackageScope Scope { get; }
    }
}
