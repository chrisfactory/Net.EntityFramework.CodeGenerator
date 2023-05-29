using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class TableTarget : Target, ITableTarget
    {
        public TableTarget(IPackageToken token) : base(token)
        {
        }
    }
}
