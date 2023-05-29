using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SqlSpSelectTarget : Target, IDbProjSpSelectTarget
    {
        public SqlSpSelectTarget(IPackageToken token) : base(token)
        {
        }
    }
}
