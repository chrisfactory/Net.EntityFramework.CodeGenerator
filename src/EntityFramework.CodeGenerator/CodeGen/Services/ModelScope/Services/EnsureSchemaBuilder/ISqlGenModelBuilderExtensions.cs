using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder Schemas(this ISqlGenModelBuilder genBuilder)
        {
            return genBuilder.AddGenActionBuilder<IEnsureSchemaBuilder, EnsureSchemaBuilder>();
        }
    }




    public interface IEnsureSchemaBuilder : IActionBuilder
    {
    }

    internal class EnsureSchemaBuilder : ActionBuilder, IEnsureSchemaBuilder
    {

        public override IActionProvider Build()
        {
            Services.AddSingleton<IEnsureSchemaOperationsProvider, EnsureSchemaOperationsProvider>();
            Services.AddSingleton<IActionProvider, EnsureSchemaSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }

    public interface IEnsureSchemaOperationsProvider
    {
        IReadOnlyCollection<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> Get();
    }
    internal class EnsureSchemaOperationsProvider : IEnsureSchemaOperationsProvider
    {
        private readonly IDbContextModelExtractor _modelExtractor;
        public EnsureSchemaOperationsProvider(IDbContextModelExtractor modelExtractor)
        {
            _modelExtractor = modelExtractor;
        }
        public IReadOnlyCollection<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> Get()
        {
            return _modelExtractor.EnsureSchemaIntents;
        }
    }

    internal class EnsureSchemaSqlGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly IEnsureSchemaOperationsProvider _commandProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public EnsureSchemaSqlGenActionProvider(
            IServiceProvider provider,
            IFileInfoFactory fileInfoProvider,
            IEnsureSchemaOperationsProvider commandProvider,
            IDbContextModelExtractor modelExtractor)
        {
            _provider = provider;
            _fileInfoProvider = fileInfoProvider;
            _commandProvider = commandProvider;
            _modelExtractor = modelExtractor;
        }
        public IEnumerable<IAction> Get()
        {
            foreach (var cmd in _commandProvider.Get())
            {
                var rootSqlPath = _modelExtractor.DefaultSqlTargetOutput.RootPath;
                var tablePattern = _modelExtractor.DefaultSqlTargetOutput.SchemasPatternPath;
                var name = cmd.Operation.Name;
                var fi = _fileInfoProvider.CreateSqlFileInfo(rootSqlPath, tablePattern, null, name);

                var fileProvider = _provider.GetRequiredService<ICreateFileActionBuilder>()
                                      .UseCommandText(cmd.Command.CommandText)
                                      .UseTargetFiles(fi)
                                      .Build();

                foreach (var fileAction in fileProvider.Get())
                    yield return fileAction;
            }
        }
    }
}
