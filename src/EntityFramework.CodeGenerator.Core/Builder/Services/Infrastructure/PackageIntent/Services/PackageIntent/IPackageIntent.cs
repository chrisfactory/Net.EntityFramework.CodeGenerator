namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageIntent
    {
        IPackageIdentity Identity { get; }
        IPackageContentSource ContentSource { get; }
        IPackageTarget Target { get; }
        IPackageContent Content { get; }
    } 
}
