using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SqlSpUpdatePackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelContext _context;
        private readonly IEntityTypeTable _entityTable;
        private readonly IDataProjectFileInfoFactory _fileInfoFactory;
        private readonly ISpUpdateParametersProvider _parametersProvider;
        private readonly IStoredProcedureSchemaProvider _schemaProvider;
        private readonly IStoredProcedureNameProvider _spNameProvider;
        public SqlSpUpdatePackageContentProvider(
            IDbContextModelContext context,
            IMutableEntityType mutableEntity,
            IDataProjectFileInfoFactory fiFoctory,
            ISpUpdateParametersProvider parametersProvider,
            IStoredProcedureSchemaProvider schemaProvider,
            IStoredProcedureNameProvider spNameProvider)
        {
            _context = context;
            _entityTable = context.GetEntity(mutableEntity);
            _fileInfoFactory = fiFoctory;
            _parametersProvider = parametersProvider;
            _schemaProvider = schemaProvider;
            _spNameProvider = spNameProvider;
        }


        public IEnumerable<IContent> Get()
        {
            var dbProjOptions = _context.DataProjectTargetInfos;
            var rootPath = dbProjOptions.RootPath;
            var pattern = dbProjOptions.StoredProceduresPatternPath;
            var spSchema = _schemaProvider.Get();
            var spName = _spNameProvider.Get();
            var targetTableFullName = _entityTable.TableFullName;
            var tableName = _entityTable.Table.Name;
            var parameters = _parametersProvider.GetParameters();
            var update = _parametersProvider.GetUpdate();
            var output = _parametersProvider.GetOutput();
            var where = _parametersProvider.GetWhere();
            var fileName = spName;
            var spOptions = new StoredProcedureOptions(spSchema, spName);
            var sp = new StoredProcedureGenerator(spOptions, parameters, new UpdateGenerator(targetTableFullName, update, output, where));
            var spCode = sp.Build();

            var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, spSchema, tableName, null, spName);
            yield return new ContentFile(fi, new CommandTextSegment(spCode));

        }
    }

}
