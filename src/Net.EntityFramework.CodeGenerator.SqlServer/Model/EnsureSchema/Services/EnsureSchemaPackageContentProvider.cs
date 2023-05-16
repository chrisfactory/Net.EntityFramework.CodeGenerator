using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaPackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelExtractor _context;
        public EnsureSchemaPackageContentProvider(IDbContextModelExtractor context)
        {
            _context = context;
        }

        public IEnumerable<IContent> Get()
        {
            foreach (var cmd in _context.EnsureSchemaIntents)
                yield return new CommandTextSegment(cmd.Command.CommandText);
        }
    }
}
