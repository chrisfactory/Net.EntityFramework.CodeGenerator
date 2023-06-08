using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageContentProvider : IIntentContentProvider
    {
        private readonly IMutableEntityType _entity;
        private readonly IDbContextModelContext _context;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        public CreateTablePackageContentProvider(
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
            var name = _entity.GetTableName();

            foreach (var cmd in _context.CreateTableIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Name == name)
                {
                    var dbProjOptions = _context.DataProjectTargetInfos;
                    var rootPath = dbProjOptions.RootPath;
                    var pattern = dbProjOptions.TablesPatternPath;
                    var fileName = cmd.Operation.Name;
                    var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, name, null, null);
                    yield return new ContentFile(fi, new CommandTextSegment(cmd.Command.CommandText));
                } 
            }
        }
    }
}
