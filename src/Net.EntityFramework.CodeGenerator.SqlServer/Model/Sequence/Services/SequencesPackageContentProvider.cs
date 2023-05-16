using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesPackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelExtractor _context;
        public SequencesPackageContentProvider(IDbContextModelExtractor context)
        {
            _context = context;
        }

        public IEnumerable<IContent> Get()
        {
            foreach (var cmd in _context.CreateSequenceIntents)
                yield return new CommandTextSegment(cmd.Command.CommandText);
        }
    }
}
