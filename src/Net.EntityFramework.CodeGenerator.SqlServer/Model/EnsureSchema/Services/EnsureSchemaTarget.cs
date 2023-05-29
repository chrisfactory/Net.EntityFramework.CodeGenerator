using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaTarget : Target, IEnsureSchemaTarget
    {
        public EnsureSchemaTarget(IPackageToken token) : base(token)
        {
        }
    }
}
