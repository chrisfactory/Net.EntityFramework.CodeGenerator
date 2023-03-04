namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageIntent
    {
        IPackageTarget Target { get; }
        IPackageContent Content { get; }
    } 
}
