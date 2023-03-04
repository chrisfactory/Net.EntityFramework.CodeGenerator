namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleProvider
    {
        IEnumerable<IPackageIntent> Get();
    }
}
