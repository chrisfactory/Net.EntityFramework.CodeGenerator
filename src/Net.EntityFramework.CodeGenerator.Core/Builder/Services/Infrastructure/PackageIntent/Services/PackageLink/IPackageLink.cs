namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageLink
    {
        IEnumerable<IPackage> Packages { get; }
    }
}
