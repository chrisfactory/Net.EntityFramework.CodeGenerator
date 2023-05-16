namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleBuilder : IBuilder<IModulePackage>
    {
        IPackageTokenProvider PackageTokenProvider { get; }
    }
}
