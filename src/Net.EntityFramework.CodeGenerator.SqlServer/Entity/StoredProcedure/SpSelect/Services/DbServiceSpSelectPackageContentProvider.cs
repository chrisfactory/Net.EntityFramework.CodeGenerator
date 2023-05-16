using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSpSelectPackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelExtractor _context;
        public DbServiceSpSelectPackageContentProvider(IDbContextModelExtractor context, ISpSelectCodeGeneratorSource src)
        {
            _context = context;
        }

        public IEnumerable<IContent> Get()
        { 

            yield return new CommandTextSegment("DbService ");
        }
    }
}
