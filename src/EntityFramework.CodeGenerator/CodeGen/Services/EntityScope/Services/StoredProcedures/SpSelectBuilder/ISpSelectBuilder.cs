using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public interface ISpSelectBuilder : IActionBuilder
    {

    }

    internal class SpSelectBuilder : ActionBuilder, ISpSelectBuilder
    {

        protected override void LoadDefaultServices()
        {
            base.LoadDefaultServices();
            this.Services.AddSingleton<ISpNameProvider, SpSelectNameProvider>();
        }

        public override IActionProvider Build()
        {
           
            Services.AddSingleton<IActionProvider, SpSelectSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }



    internal class SpSelectSqlGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IEntityTypeTable _entity;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly ISpNameProvider _spNameProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public SpSelectSqlGenActionProvider(
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

            var rootPath = _modelExtractor.DefaultSqlTargetOutput.RootPath;
            var pattern = _modelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath;
            var name = _entity.Table.Name;
            var schema = _entity.Table.Schema;

            var fi = _fileInfoProvider.CreateSqlFileInfo(rootPath, pattern, schema, name, spName);

           var fileProvider = _provider.GetRequiredService<ICreateFileActionBuilder>()
              .UseFileAction(new SpSelect(_entity, spName))
              .UseTargetFiles(fi)
              .Build(); 

            foreach (var fileAction in fileProvider.Get())
                yield return fileAction;
        }
    }
}