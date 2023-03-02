namespace EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleIntent : IPackageModuleIntent
    {
        public PackageModuleIntent(IEnumerable<IPackageIntent> intents)
        {
            Intents = intents;
        }

        public IEnumerable<IPackageIntent> Intents { get; }
    }
}
