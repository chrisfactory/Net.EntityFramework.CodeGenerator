using System.Diagnostics;

namespace EntityFramework.CodeGenerator.Core
{
    [DebuggerDisplay("{Identity}")]
    internal class PackageModuleIntent : IPackageModuleIntent
    {
        public PackageModuleIntent(IPackageContentSource source, IPackageIdentity identity, IEnumerable<IPackageIntent> intents)
        {
            ContentSource = source;
            Identity = identity;
            Intents = intents;
        }
        public IPackageIdentity Identity { get; }
        public IPackageContentSource ContentSource { get; }
        public IEnumerable<IPackageIntent> Intents { get; }


    }
}
