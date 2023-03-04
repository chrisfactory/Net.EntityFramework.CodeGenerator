using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
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
