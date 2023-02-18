using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder AllSchema(this ISqlGenModelBuilder genBuilder)
        {
            return genBuilder.AddGenActionBuilder<IEnsureSchemaBuilder, EnsureSchemaBuilder>();
        }
    }




    public interface IEnsureSchemaBuilder : ISqlGenActionBuilder
    {
    }

    internal class EnsureSchemaBuilder : SqlGenActionBuilder, IEnsureSchemaBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            Services.AddSingleton<ISqlFileInfoFactory, GetSqlFileInfoFactory>();
            Services.AddSingleton<IEnsureSchemaOperationsProvider, EnsureSchemaOperationsProvider>();
            Services.AddSingleton<ISqlGenActionProvider, EnsureSchemaSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
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

    internal class EnsureSchemaSqlGenActionProvider : ISqlGenActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly ISqlFileInfoFactory _fileInfoProvider;
        private readonly IEnsureSchemaOperationsProvider _commandProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public EnsureSchemaSqlGenActionProvider(
            IServiceProvider provider,
            ISqlFileInfoFactory fileInfoProvider,
            IEnsureSchemaOperationsProvider commandProvider,
            IDbContextModelExtractor modelExtractor)
        {
            _provider = provider;
            _fileInfoProvider = fileInfoProvider;
            _commandProvider = commandProvider;
            _modelExtractor = modelExtractor;
        }
        public IEnumerable<ISqlGenAction> Get()
        {
            foreach (var cmd in _commandProvider.Get())
            {
                var rootSqlPath = _modelExtractor.DefaultSqlTargetOutput.RootPath;
                var tablePattern = _modelExtractor.DefaultSqlTargetOutput.SchemasPatternPath;
                var name = cmd.Operation.Name;
                var fi = _fileInfoProvider.CreateFileInfo(rootSqlPath, tablePattern, null, name);

                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                                      .UseCommandText(cmd.Command.CommandText)
                                      .UseTargetFiles(fi)
                                      .Build();
            }


        }
    }




}
