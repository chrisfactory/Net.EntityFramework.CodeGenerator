using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public interface IDbServiceBuilder : IActionBuilder
    {
    }

    internal class DbServiceBuilder : ActionBuilder, IDbServiceBuilder
    {

        public override IActionProvider Build()
        {
            throw new NotImplementedException();
        }
    }




    internal class DbServiceCsGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IEntityTypeTable _entity;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly ISpNameProvider _spNameProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public DbServiceCsGenActionProvider(
            IServiceProvider provider,
            IEntityTypeTable entity,
            IFileInfoFactory fileInfoProvider,
            ISpNameProvider spNameProvider,
            IDbContextModelExtractor modelExtractor)
        {
            _provider = provider;
            _entity = entity;
            _fileInfoProvider = fileInfoProvider;
            _spNameProvider = spNameProvider;
            _modelExtractor = modelExtractor;
        }
        public IEnumerable<IAction> Get()
        {
            var spName = _spNameProvider.Get();

            var rootSqlPath = _modelExtractor.DefaultSqlTargetOutput.RootPath;
            var tablePattern = _modelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath;
            var name = _entity.Table.Name;
            var schema = _entity.Table.Schema;

            var fi = _fileInfoProvider.CreateCsFileInfo(rootSqlPath, tablePattern, schema, name, spName);




            var fileProvider = _provider.GetRequiredService<ICreateFileActionBuilder>()
                 .UseFileAction(new SpInsert(_entity, spName))
                 .UseTargetFiles(fi)
                 .Build();

            foreach (var fileAction in fileProvider.Get())
                yield return fileAction;

            //var insertMap = new CsCallInsertPs(spInsert, entity);
            //var selectMap = new CsCallSelectPs(spSelect, entity);
            //var updateMap = new CsCallUpdatetPs(spUpdate, entity);
            //var deleteMap = new CsCallDeletePs(spDelete, entity);
            //string className = $"{item.Operation.Name}DbService";
            //new CsDbAccess(className, _entity, insertMap, selectMap, updateMap, deleteMap);

            //yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
            //  .UseFileAction()
            //  .UseTargetFiles(GetCsFileInfo(rootCsPath, _ModelExtractor.DefaultCsTargetOutput.DbServicePatternPath, item.Operation.Schema, item.Operation.Name, className))
            //.Build();
        }
    }
}
