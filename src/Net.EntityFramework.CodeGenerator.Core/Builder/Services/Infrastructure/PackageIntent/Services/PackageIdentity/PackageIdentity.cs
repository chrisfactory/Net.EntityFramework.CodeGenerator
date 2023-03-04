using System.Diagnostics;

namespace Net.EntityFramework.CodeGenerator.Core
{
    [DebuggerDisplay("{Name}")]
    public class PackageIdentity : IPackageIdentity
    {
        public PackageIdentity(IPackageContentSource src)
        {
            Name = $"{src.Name} {src.Scope.GetDisplayName()}";
        }
        public string Name { get; }
    }
}
