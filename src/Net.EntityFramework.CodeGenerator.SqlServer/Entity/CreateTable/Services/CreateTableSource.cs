using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTableSource : ICreateTableSource
    {
        public string Name { get; } = "Create Table";
    }
}
