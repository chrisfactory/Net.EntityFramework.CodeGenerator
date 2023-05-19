namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class PackageTokenProvider : IPackageTokenProvider
    {
        private readonly Guid SourceKey = Guid.NewGuid();

        public bool Checked(IPackageToken token)
        {
          return token != null && token.Source == SourceKey;
        }

        public IPackageToken CreateToken() => new PackageToken(SourceKey);

        internal class PackageToken : IPackageToken
        {
            private bool _cw = false;
            private List<IPackageToken> _CorrelateTokens = new List<IPackageToken>();
            public PackageToken(Guid source)
            {
                Source = source;
                Token = Guid.NewGuid();
            }
            public Guid Source { get; }
            public Guid Token { get; }

            public IEnumerable<IPackageToken> CorrelateTokens { get { return (IReadOnlyList<IPackageToken>)_CorrelateTokens; } }

            public IPackageToken CorrelateWith(params IPackageToken[] with)
            {
                lock (_CorrelateTokens)
                {
                    if (_cw) throw new InvalidOperationException();
                    _cw = true;
                    if (with != null)
                        _CorrelateTokens = with.Distinct().ToList();
                }
                return this;
            }

            public override string ToString() => $"{Token}";

        }
    }
}
