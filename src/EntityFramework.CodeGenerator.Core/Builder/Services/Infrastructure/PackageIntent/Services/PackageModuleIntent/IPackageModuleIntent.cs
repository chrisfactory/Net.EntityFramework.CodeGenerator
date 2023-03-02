namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleIntent
    {
        IEnumerable<IPackageIntent> Intents { get; }
    }
}
