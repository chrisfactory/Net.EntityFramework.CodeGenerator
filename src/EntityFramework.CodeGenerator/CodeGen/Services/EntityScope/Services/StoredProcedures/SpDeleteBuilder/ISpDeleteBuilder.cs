using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;
using EntityFramework.CodeGenerator.Core;
namespace EntityFramework.CodeGenerator
{
    public interface ISpDeleteBuilder : IActionBuilder
    {
    }

    internal class SpDeleteBuilder : ActionBuilder, ISpDeleteBuilder
    {
        protected override void LoadDefaultServices()
        {
            base.LoadDefaultServices();
            this.Services.AddSingleton<ISpNameProvider, SpDeleteNameProvider>();
        }

        public override IActionProvider Build()
        {

            Services.AddSingleton<IActionProvider, SpDeleteSqlGenActionProvider>();
            Services.AddSingleton<IStoredProcedureCallerProvider, SpDeleteCallerActionProvider>();
            Services.AddSingleton<IStoredProcedureActionProvider, StoredProcedureActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IStoredProcedureActionProvider>();
        }

    }

    internal class SpDeleteSqlGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IEntityTypeTable _entity;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly ISpNameProvider _spNameProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public SpDeleteSqlGenActionProvider(
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
                .UseFileAction(new SpDelete(_entity, spName))
                .UseTargetFiles(fi)
                .Build();

            foreach (var fileAction in fileProvider.Get())
                yield return fileAction;
        }

    }

    internal class SpDeleteCallerActionProvider : IStoredProcedureCallerProvider
    {
        private readonly IEntityTypeTable _entity;
        private readonly ISpNameProvider _spNameProvider;
        public SpDeleteCallerActionProvider(
            IEntityTypeTable entity,
            ISpNameProvider spNameProvider)
        {
            _entity = entity;
            _spNameProvider = spNameProvider;
        }
        public IEnumerable<IContentFileSegment> Get()
        {
            var spName = _spNameProvider.Get();
            yield return new CsCallDeletePs(spName, _entity);
        }
    }
}
