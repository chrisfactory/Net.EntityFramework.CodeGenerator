namespace Net.EntityFramework.CodeGenerator.Core
{
    public class Package : IPackage
    {
        public Package(IPackageToken token, IEnumerable<IIntent> intents)
        {
            Token = token; 
            Intents = intents;
        }
        public IPackageToken Token { get; }
        public IEnumerable<IIntent> Intents { get; }
    }
}
