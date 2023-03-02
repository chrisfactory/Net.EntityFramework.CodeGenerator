using System.Diagnostics;

namespace EntityFramework.CodeGenerator.Core
{
    [DebuggerDisplay("{Identity}")]
    internal class PackageIntent : IPackageIntent
    {
        public PackageIntent(IPackageIdentity identity, IPackageContentSource source, IPackageTarget target, IPackageContent content)
        {
            Identity = identity;
            ContentSource = source;
            Target = target;
            Content = content;
        }
         

        public IPackageIdentity Identity { get; }

        public IPackageContentSource ContentSource { get; }

        public IPackageTarget Target { get; }

        public IPackageContent Content { get; }

    }
}
