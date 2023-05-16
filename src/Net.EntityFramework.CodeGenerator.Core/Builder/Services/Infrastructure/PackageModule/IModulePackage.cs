namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IModulePackage
    {
        IEnumerable<IPackage> Packages { get; }
    }
    internal class Module : IModulePackage
    {
        public Module(IEnumerable<IPackage> packages)
        {
            Packages = packages;
        }
        public IEnumerable<IPackage> Packages { get; }
    }
}