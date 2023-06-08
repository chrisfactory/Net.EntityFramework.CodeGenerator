using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexPackageContentProvider : IIntentContentProvider
    {
        private readonly IMutableEntityType _entity;
        private readonly IDbContextModelContext _context;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        public CreateIndexPackageContentProvider(
               IMutableEntityType entity,
               IDbContextModelContext context,
               IDataProjectFileInfoFactory fiFoctory)
        {
            _entity = entity;
            _context = context;
            _fileInfoFactory = fiFoctory;
        }

        public IEnumerable<IContent> Get()
        { 
            var schema = _entity.GetSchema();
            var tableName = _entity.GetTableName();
            foreach (var cmd in _context.CreateIndexIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Table == tableName)
                {
                    var dbProjOptions = _context.DataProjectTargetInfos;
                    var rootPath = dbProjOptions.RootPath;
                    var pattern = dbProjOptions.IndexsPatternPath;
                    var fileName = cmd.Operation.Name;
                    var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, tableName, null, null);
                    yield return new ContentFile(fi, new CommandTextSegment(cmd.Command.CommandText));
                }
            }
        }
    }
}
