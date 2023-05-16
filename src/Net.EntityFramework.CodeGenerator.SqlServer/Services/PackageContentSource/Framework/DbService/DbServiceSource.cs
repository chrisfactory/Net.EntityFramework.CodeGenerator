using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSource : IDbServiceCodeGeneratorSource
    {
        public string Name { get; } = "Db Service";
    }
}
