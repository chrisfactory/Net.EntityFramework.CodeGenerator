namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageIdentity
    {
        string Name { get; }

        IPackageToken Token { get; }
    }
}
