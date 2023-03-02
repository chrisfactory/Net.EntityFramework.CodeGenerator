namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleIntentsProvider
    {
        IEnumerable<IPackageModuleIntent> Get();
    }
}
