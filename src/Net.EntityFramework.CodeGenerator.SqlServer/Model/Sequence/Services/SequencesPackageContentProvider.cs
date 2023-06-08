using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesPackageContentProvider : IIntentContentProvider
    { 
        private readonly IDbContextModelContext _context;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        public SequencesPackageContentProvider( 
               IDbContextModelContext context,
               IDataProjectFileInfoFactory fiFoctory)
        { 
            _context = context;
            _fileInfoFactory = fiFoctory;
        }


        public IEnumerable<IContent> Get()
        { 
            foreach (var cmd in _context.CreateSequenceIntents)
            {

                var schema = cmd.Operation.Schema;

                var dbProjOptions = _context.DataProjectTargetInfos;
                var rootPath = dbProjOptions.RootPath;
                var pattern = dbProjOptions.SequencesPatternPath;
                var fileName = cmd.Operation.Name;
                var sequenceName = cmd.Operation.Name;
                var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, null, sequenceName, null);
                yield return new ContentFile(fi, new CommandTextSegment(cmd.Command.CommandText));
            }

        }
    }
}
