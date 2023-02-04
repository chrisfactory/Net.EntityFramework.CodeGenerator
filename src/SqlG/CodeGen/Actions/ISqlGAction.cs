namespace SqlG
{
    public interface ISqlGAction
    {
        Task ExecuteAsync(CancellationToken token);
    }
}
