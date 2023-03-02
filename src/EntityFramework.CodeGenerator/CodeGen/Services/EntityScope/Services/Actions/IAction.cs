namespace EntityFramework.CodeGenerator
{
    public interface IAction
    {
        Task ExecuteAsync(CancellationToken token);
    }
}
