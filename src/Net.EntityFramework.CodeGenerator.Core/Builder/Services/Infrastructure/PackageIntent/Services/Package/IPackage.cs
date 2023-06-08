namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackage
    {
        IPackageToken Token { get; } 
        IEnumerable<IIntent> Intents { get; }
    }
}