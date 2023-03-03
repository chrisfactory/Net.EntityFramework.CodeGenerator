namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleIntent
    {
        IPackageIdentity Identity { get; }
        IPackageContentSource ContentSource { get; }
        IEnumerable<IPackageIntent> Intents { get; }
    }
}
