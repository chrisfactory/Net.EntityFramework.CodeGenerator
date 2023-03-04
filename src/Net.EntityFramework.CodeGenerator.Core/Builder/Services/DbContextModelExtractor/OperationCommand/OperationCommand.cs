namespace Net.EntityFramework.CodeGenerator.Core
{
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
}
