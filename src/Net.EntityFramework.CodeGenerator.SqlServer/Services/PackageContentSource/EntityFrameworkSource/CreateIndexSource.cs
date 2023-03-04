using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexSource : ICreateIndexSource
    {
        public CreateIndexSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Create Index";
        public IPackageScope Scope { get; }
    }
}
