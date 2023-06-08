using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServicePackageContentProvider : IIntentContentProvider
    { 
        public IEnumerable<IContent> Get()
        {
            yield return new CommandTextSegment("DbService");
        }
    }


}
