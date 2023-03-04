namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IOperationCommand<TOperation, TCommand>
    {
        TOperation Operation { get; }
        TCommand Command { get; }
    }
}
