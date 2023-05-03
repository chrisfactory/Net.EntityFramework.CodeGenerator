using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SqlSpSelectTarget : IDbProjSpSelectTarget
    {
        public SqlSpSelectTarget(IStoredProcedureNameProvider n) { 
        }
    }
}
