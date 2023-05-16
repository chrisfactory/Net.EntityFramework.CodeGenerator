using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServicePackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelExtractor _context;
        public DbServicePackageContentProvider(IDbContextModelExtractor context)
        {
            _context = context;
        }

        public IEnumerable<IContent> Get()
        {
            yield return new CommandTextSegment("DbService");
        }
    }
}
