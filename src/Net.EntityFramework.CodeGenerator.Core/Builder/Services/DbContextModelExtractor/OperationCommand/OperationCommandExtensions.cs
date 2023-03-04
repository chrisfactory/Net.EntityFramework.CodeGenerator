using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal static class IOperationCommandExtensions
    {
        public static string GetTableFullName(this IOperationCommand<CreateTableOperation, MigrationCommand> cmd)
        {
            return EntityTypeTable.GetTableFullName(cmd.Operation.Schema, cmd.Operation.Name);
        }

        public static string GetTableFullName(this IOperationCommand<CreateIndexOperation, MigrationCommand> cmd)
        {
            return EntityTypeTable.GetTableFullName(cmd.Operation.Schema, cmd.Operation.Table);
        }
    }
}
