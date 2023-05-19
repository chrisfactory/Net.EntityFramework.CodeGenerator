namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageBuilder : IBuilder<IPackage>
    { 
    }
    public interface IPackageBuilderKey
    {
        IPackageToken Token { get; }
        IPackageBuilder Builder {  get; }
    }
}
