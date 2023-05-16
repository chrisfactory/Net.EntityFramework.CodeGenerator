namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackage
    {
        IPackageToken Token { get; }
        IPackageSource Source { get; }
        IEnumerable<IIntent> Intents { get; }
    }
}