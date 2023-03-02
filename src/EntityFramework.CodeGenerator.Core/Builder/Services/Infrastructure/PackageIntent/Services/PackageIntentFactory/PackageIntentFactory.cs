namespace EntityFramework.CodeGenerator.Core
{
    internal class PackageIntentFactory : IPackageIntentFactory
    {
        private readonly IEnumerable<IPackageIntentBuilder> _builders;
        public PackageIntentFactory(IEnumerable<IPackageIntentBuilder> builders)
        {
            _builders = builders;
        }
        public IEnumerable<IPackageIntent> Create()
        {
            foreach (var builder in _builders)
                yield return builder.Build();
        }
    }
}
