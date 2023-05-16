namespace Net.EntityFramework.CodeGenerator.Core
{
    public class Package : IPackage
    {
        public Package(IPackageToken token, IPackageSource source, IEnumerable<IIntent> intents)
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
