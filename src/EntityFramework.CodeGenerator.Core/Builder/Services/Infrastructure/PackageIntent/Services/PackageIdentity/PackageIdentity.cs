using EntityFramework.CodeGenerator.Core;

namespace EntityFramework.CodeGenerator
{
    internal class PackageIdentity : IPackageIdentity
    {
        public PackageIdentity(IPackageContentSource src)
        {
            Name = $"{src.Name}::{ src.Scope.GetDisplayName()}";
        }
        public string Name { get; }
    }
}
