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
}
