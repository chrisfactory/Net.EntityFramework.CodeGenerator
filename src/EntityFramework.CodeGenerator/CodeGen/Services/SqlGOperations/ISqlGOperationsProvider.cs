namespace EntityFramework.CodeGenerator
{
    public interface ISqlGOperationsProvider
    {
        IEnumerable<ISqlGenAction> GetOperations();
    }
}
