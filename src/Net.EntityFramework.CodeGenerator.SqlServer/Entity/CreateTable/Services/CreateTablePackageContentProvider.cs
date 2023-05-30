using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageContentProvider : IIntentContentProvider
    {
        private readonly ICreateTableSource _source;
        private readonly IDbContextModelExtractor _context;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        public CreateTablePackageContentProvider(
            ICreateTableSource source,
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
            var name = _source.Name;
            foreach (var cmd in _context.CreateTableIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Name == name)
                {
                    var dbProjOptions = _context.DataProjectTargetInfos;
                    var rootPath = dbProjOptions.RootPath;
                    var pattern = dbProjOptions.TablesPatternPath;
                    var fileName = cmd.Operation.Name;
                    var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, name, null);
                    yield return new ContentFile(fi, new CommandTextSegment(cmd.Command.CommandText));
                }

            }
        }
    }
}
