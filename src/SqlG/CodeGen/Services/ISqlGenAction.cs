namespace SqlG
{
    public interface ISqlGenActionProvider
    {
        IEnumerable<ISqlGenAction> Get(); 
    }
}
