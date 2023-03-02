using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public interface ISpUpdateBuilder : IActionBuilder
    {
    }

    internal class SpUpdateBuilder : ActionBuilder, ISpUpdateBuilder
    {
        protected override void LoadDefaultServices()
        {
            base.LoadDefaultServices();
            this.Services.AddSingleton<ISpNameProvider, SpUpdateNameProvider>();
        }

        public override IActionProvider Build()
        {

            Services.AddSingleton<IActionProvider, SpUpdateSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }

    internal class SpUpdateSqlGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IEntityTypeTable _entity;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly ISpNameProvider _spNameProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public SpUpdateSqlGenActionProvider(
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

            var fi = _fileInfoProvider.CreateSqlFileInfo(rootSqlPath, tablePattern, schema, name, spName);

            var fileProvider = _provider.GetRequiredService<ICreateFileActionBuilder>()
              .UseFileAction(new SpUpdate(_entity, spName))
              .UseTargetFiles(fi)
              .Build();

            foreach (var fileAction in fileProvider.Get())
                yield return fileAction;

        }
    }
}
