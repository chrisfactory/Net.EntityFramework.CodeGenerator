namespace SqlG
{
    public interface ISqlGenAction
    {
        Task ExecuteAsync(CancellationToken token);
    }
}
