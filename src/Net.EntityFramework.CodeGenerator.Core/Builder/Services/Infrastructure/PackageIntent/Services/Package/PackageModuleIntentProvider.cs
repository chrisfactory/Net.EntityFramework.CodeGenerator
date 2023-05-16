namespace Net.EntityFramework.CodeGenerator.Core
{
    public class PackageModuleIntent : IPostBuildPackage
    {
        public PackageModuleIntent(IPackageToken token, IPackageSource source, IEnumerable<IIntent> intents)
        {
            Token = token;
            Source = source;
            Intents = intents;
        }
        public IPackageToken Token { get; }
        public IPackageSource Source { get; }
        public IEnumerable<IIntent> Intents { get; }
    }
}
