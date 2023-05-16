namespace Net.EntityFramework.CodeGenerator.Core
{
    public class TokenLink : ITokenLink
    {
        public TokenLink(IEnumerable<IPackageToken> tokens)
        {
            Tokens = tokens;
        }
        public IEnumerable<IPackageToken> Tokens { get; }
    }
}
