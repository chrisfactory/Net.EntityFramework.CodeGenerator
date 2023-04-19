using System.Diagnostics;

namespace Net.EntityFramework.CodeGenerator.Core
{
    [DebuggerDisplay("{Name}")]
    public class PackageIdentity : IPackageIdentity
    {
        public PackageIdentity(IPackageContentSource src, IPackageToken token)
        {
            Name = $"{src.Name} {src.Scope.GetDisplayName()}";
            Token = token;
        }
        public string Name { get; }

        public IPackageToken Token { get; }
    }
}
