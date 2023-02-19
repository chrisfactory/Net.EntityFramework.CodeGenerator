namespace EntityFramework.CodeGenerator
{
    public interface ISqlGenAction
    {
        Task ExecuteAsync(CancellationToken token);
    }
}
