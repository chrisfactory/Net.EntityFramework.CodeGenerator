using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexPackageContentProvider : IIntentContentProvider
    {
        private readonly IMutableEntityType _entity;
        private readonly IDbContextModelExtractor _context;
        public CreateIndexPackageContentProvider(IMutableEntityType entity, IDbContextModelExtractor context)
        {
            _entity = entity;
            _context = context;
        }

        public IEnumerable<IContent> Get()
        {
            var schema = _entity.GetSchema();
            var tableName = _entity.GetTableName();
            foreach (var cmd in _context.CreateIndexIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Table == tableName)
                    yield return new CommandTextSegment(cmd.Command.CommandText);
            }
        }
    }
}
