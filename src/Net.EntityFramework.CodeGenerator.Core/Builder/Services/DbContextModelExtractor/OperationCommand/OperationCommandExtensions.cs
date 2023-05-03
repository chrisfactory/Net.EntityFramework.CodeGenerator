using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator
{
    public static class IOperationCommandExtensions
    {

        public static string GetTableFullName(this IMutableEntityType entity)
        {
            return GetTableFullName(entity.GetSchema(), entity.GetTableName());
        } 

        public static string GetTableFullName(this ITable table)
        {
            return GetTableFullName(table.Schema, table.Name);
        }

        private static string GetTableFullName(string? schema, string? tableName)
        {
            var s = string.IsNullOrEmpty(schema) ? "" : $"[{schema}].";
            return $"{s}[{tableName}]";
        }
        //public static string GetTableFullName(this IOperationCommand<CreateTableOperation, MigrationCommand> cmd)
        //{
        //    return GetTableFullName(cmd.Operation.Schema, cmd.Operation.Name);
        //}

        //public static string GetTableFullName(this IOperationCommand<CreateIndexOperation, MigrationCommand> cmd)
        //{
        //    return GetTableFullName(cmd.Operation.Schema, cmd.Operation.Table);
        //}
    }
}
