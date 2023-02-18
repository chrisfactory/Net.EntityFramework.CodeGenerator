namespace SqlG
{
    public interface ISqlGOperationsProvider
    {
        IEnumerable<ISqlGenAction> GetOperations();
    }
}
