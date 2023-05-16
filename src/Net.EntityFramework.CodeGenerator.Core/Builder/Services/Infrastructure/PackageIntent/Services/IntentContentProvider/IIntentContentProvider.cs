namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IIntentContentProvider
    {
        IEnumerable<IContent> Get();
    }
}
