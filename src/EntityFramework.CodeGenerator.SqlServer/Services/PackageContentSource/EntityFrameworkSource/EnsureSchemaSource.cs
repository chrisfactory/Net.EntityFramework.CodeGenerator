using EntityFramework.CodeGenerator.Core;

namespace EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaSource : IEnsureSchemaSource
    {
        public EnsureSchemaSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Ensure Schema";
        public IPackageScope Scope { get; }
    }
}
