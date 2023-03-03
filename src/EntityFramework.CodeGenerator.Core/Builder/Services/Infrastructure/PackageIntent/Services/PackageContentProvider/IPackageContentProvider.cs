namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageContentProvider
    {
        IEnumerable<IPackageContent> Get();
    }
}
