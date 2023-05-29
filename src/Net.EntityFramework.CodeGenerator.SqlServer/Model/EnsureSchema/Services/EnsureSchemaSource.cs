using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaSource : IEnsureSchemaSource
    {
        public string Name { get; } = "Ensure Schema";
    }
}
