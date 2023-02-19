namespace EntityFramework.CodeGenerator
{
    public interface ISqlGenActionProvider
    {
        IEnumerable<ISqlGenAction> Get(); 
    }
}
