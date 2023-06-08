using Net.EntityFramework.CodeGenerator.Core;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSpSelectPackageContentProvider : IIntentContentProvider
    {
        public DbServiceSpSelectPackageContentProvider()
        { 
        }

        public IEnumerable<IContent> Get()
        {
            yield return new SpSelectStoredProcedureInfos();
        }
    }
     
    internal class SpSelectStoredProcedureInfos : IContent
    {
        public SpSelectStoredProcedureInfos( )
        { 
        }  
    
    }
}
