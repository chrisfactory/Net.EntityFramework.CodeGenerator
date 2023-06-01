using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaPackageContentProvider : IIntentContentProvider
    {
        private readonly IEnsureSchemaSource _source;
        private readonly IDbContextModelContext _context;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        public EnsureSchemaPackageContentProvider(
               IEnsureSchemaSource source,
               IDbContextModelContext context,
               IDataProjectFileInfoFactory fiFoctory)
        {
            _source = source;
            _context = context;
            _fileInfoFactory = fiFoctory;
        }

        public IEnumerable<IContent> Get()
        {
            foreach (var cmd in _context.EnsureSchemaIntents)
            {
                var schema = cmd.Operation.Name;

                var dbProjOptions = _context.DataProjectTargetInfos;
                var rootPath = dbProjOptions.RootPath;
                var pattern = dbProjOptions.SchemasPatternPath;
                var fileName = cmd.Operation.Name;
                var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, null, null, null);
                yield return new ContentFile(fi, new CommandTextSegment(ExtractCommand(schema, cmd.Command.CommandText)));

            }
        }

        private string ExtractCommand(string schemaName, string commandText)
        {
            return $"CREATE SCHEMA [{schemaName}];";
        }
    }
}
