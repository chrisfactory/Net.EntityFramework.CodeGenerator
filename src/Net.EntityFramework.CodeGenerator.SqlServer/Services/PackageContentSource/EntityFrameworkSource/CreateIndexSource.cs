using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexSource : ICreateIndexSource
    {
        public string Name { get; } = "Create Index";
    }
}
