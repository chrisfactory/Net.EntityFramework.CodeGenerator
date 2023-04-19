namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageToken
    {
        Guid Source { get; }
        Guid Token { get; }
    }

    internal class PackageTokenProvider : IPackageTokenProvider
    {
        private readonly Guid SourceKey = Guid.NewGuid();
        public IPackageToken CreateToken() => new PackageToken(SourceKey);

        internal class PackageToken : IPackageToken
        {
            public PackageToken(Guid source)
            {
                Source = source;
                Token = Guid.NewGuid();
            }
            public Guid Source { get; }
            public Guid Token { get; }

            public override string ToString() => $"{Token}";

        }
    }

    public interface IPackageTokenProvider
    {
        IPackageToken CreateToken();
    }
    public interface IPackageModuleBuilder : IBuilder<IPackageModuleIntentProvider>
    {
        IPackageTokenProvider PackageTokenProvider { get; }
    }
}
