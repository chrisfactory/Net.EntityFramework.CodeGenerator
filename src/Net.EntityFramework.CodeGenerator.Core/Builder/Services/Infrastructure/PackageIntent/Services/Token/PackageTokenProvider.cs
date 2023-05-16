namespace Net.EntityFramework.CodeGenerator.Core
{
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
}
