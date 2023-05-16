namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface ITokenLink
    {
        IEnumerable<IPackageToken> Tokens { get; }
    }
}

