using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class IndexTarget : Target, IIndexTarget
    {
        public IndexTarget(IPackageToken token) : base(token)
        {
        }
         
    }
}
