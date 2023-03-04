namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageContentProvider
    {
        IEnumerable<IPackageContent> Get();
    }
}
