namespace EntityFramework.CodeGenerator
{
    public interface ISqlGOperationsProvider
    {
        IEnumerable<IAction> GetOperations();
    }
}
