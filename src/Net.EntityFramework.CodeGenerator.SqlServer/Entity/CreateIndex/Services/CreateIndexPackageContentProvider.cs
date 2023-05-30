using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexPackageContentProvider : IIntentContentProvider
    {
        private readonly ICreateIndexSource _source;
        private readonly IDbContextModelExtractor _context;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        public CreateIndexPackageContentProvider(
               ICreateIndexSource source,
               IDbContextModelExtractor context,
               IDataProjectFileInfoFactory fiFoctory)
        {
            _source = source;
            _context = context;
            _fileInfoFactory = fiFoctory;
        }

        public IEnumerable<IContent> Get()
        {
            var schema = _source.Schema;
            var tableName = _source.Name;
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
