namespace SqlG
{
    public interface ISqlGOperationsProvider
    {
        IEnumerable<ISqlGAction> GetOperations();
    }
}
