using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageContentProvider : IIntentContentProvider
    {
        private readonly IMutableEntityType _entity;
        private readonly IDbContextModelExtractor _context;
        public CreateTablePackageContentProvider(IMutableEntityType entity, IDbContextModelExtractor context)
        {
            _entity = entity;
            _context = context;
        }

        public IEnumerable<IContent> Get()
        {
            var schema = _entity.GetSchema();
            var tableName = _entity.GetTableName();
            foreach (var cmd in _context.CreateTableIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Name == tableName)
                    yield return new CommandTextSegment(cmd.Command.CommandText);
            }
        }
    }
}
