using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceTarget : Target, IDbServiceTarget
    {
        public DbServiceTarget(IPackageToken token) : base(token)
        {
        }
    }
}
