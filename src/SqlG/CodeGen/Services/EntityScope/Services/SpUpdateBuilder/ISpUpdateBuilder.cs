using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public interface ISpUpdateBuilder : ISqlGenActionBuilder
    {
    }

    internal class SpUpdateBuilder : SqlGenActionBuilder, ISpUpdateBuilder
    {
        protected override void LoadDefaultServices()
        {
            base.LoadDefaultServices();
            this.Services.AddSingleton<ISpNameProvider, SpUpdateNameProvider>();
        }

        public override ISqlGenActionProvider Build()
        {

            Services.AddSingleton<ISqlGenActionProvider, SpUpdateSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
        }
    } 

    internal class SpUpdateSqlGenActionProvider : ISqlGenActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IEntityTypeTable _entity;
        private readonly ISqlFileInfoFactory _fileInfoProvider;
        private readonly ISpNameProvider _spNameProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public SpUpdateSqlGenActionProvider(
            IServiceProvider provider,
            IEntityTypeTable entity,
            ISqlFileInfoFactory fileInfoProvider,
            ISpNameProvider spNameProvider,
            IDbContextModelExtractor modelExtractor)
        {
            _provider = provider;
            _entity = entity;
            _fileInfoProvider = fileInfoProvider;
            _spNameProvider = spNameProvider;
            _modelExtractor = modelExtractor;
        }
        public IEnumerable<ISqlGenAction> Get()
        {
            var spName = _spNameProvider.Get();

            var rootSqlPath = _modelExtractor.DefaultSqlTargetOutput.RootPath;
            var tablePattern = _modelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath;
            var name = _entity.Table.Name;
            var schema = _entity.Table.Schema;

            var fi = _fileInfoProvider.CreateFileInfo(rootSqlPath, tablePattern, schema, name, spName);

            yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
              .UseFileAction(new SpUpdate(_entity, spName))
              .UseTargetFiles(fi)
              .Build();

        }
    }
}
