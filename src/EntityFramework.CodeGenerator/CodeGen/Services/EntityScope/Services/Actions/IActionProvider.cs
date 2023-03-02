namespace EntityFramework.CodeGenerator
{
    public interface IActionProvider
    {
        IEnumerable<IAction> Get(); 
    }
}
