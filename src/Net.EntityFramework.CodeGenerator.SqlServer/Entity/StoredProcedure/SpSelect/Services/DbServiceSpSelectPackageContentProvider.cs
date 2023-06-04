using Net.EntityFramework.CodeGenerator.Core;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSpSelectPackageContentProvider : IIntentContentProvider
    {
        private readonly ISpSelectCodeGeneratorSource _source;
        public DbServiceSpSelectPackageContentProvider(ISpSelectCodeGeneratorSource src)
        {
            _source = src;
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
