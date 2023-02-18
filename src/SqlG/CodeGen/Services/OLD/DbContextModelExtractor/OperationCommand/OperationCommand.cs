using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlG
{
    public interface IOperationCommand<TOperation, TCommand>
    {
        TOperation Operation { get; }
        TCommand Command { get; }
    }
    internal class OperationCommand<TOperation, TCommand> : IOperationCommand<TOperation, TCommand>
    {
        public OperationCommand(TOperation operation, TCommand cmd)
        {
            Operation = operation;
            Command = cmd;
        }

        public TOperation Operation { get; }

        public TCommand Command { get; }

    }


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
