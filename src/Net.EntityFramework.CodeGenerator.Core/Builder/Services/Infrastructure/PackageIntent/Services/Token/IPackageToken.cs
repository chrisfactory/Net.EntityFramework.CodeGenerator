namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageToken
    {
        Guid Source { get; }
        Guid Token { get; }
        IEnumerable<IPackageToken> CorrelateTokens { get; }
        IPackageToken CorrelateWith(params IPackageToken[] with);
    }
}
