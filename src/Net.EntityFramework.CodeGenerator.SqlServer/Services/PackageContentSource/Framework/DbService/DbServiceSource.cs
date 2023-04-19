using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSource : IDbServiceCodeGeneratorSource
    {
        public DbServiceSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Db Service";
        public IPackageScope Scope { get; }
    }
}
