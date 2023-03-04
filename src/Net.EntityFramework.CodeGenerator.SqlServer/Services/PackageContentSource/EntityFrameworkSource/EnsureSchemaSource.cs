using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
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
