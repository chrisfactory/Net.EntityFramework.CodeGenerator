namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleBuilderProvider
    {
        IEnumerable<IPackageModuleBuilder> Get();
    }
}
