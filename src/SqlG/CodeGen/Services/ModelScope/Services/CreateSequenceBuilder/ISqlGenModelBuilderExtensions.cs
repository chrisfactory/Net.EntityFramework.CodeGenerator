using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder Sequences(this ISqlGenModelBuilder genBuilder)
        { 
            return genBuilder.AddGenActionBuilder<ICreateSequenceBuilder, CreateSequenceBuilder>();
        }
    }




    public interface ICreateSequenceBuilder : ISqlGenActionBuilder
    {
    }

    internal class CreateSequenceBuilder : SqlGenActionBuilder, ICreateSequenceBuilder
    {

        public override ISqlGenActionProvider Build()
        { 
            Services.AddSingleton<ICreateSequenceOperationsProvider, CreateSequenceOperationsProvider>();
            Services.AddSingleton<ISqlGenActionProvider, CreateSequenceSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
        }
    }

    public interface ICreateSequenceOperationsProvider
    {
        IReadOnlyCollection<IOperationCommand<CreateSequenceOperation, MigrationCommand>> Get();
    }
    internal class CreateSequenceOperationsProvider : ICreateSequenceOperationsProvider
    {
        private readonly IDbContextModelExtractor _modelExtractor;
        public CreateSequenceOperationsProvider(IDbContextModelExtractor modelExtractor)
        {
            _modelExtractor = modelExtractor;
        }
        public IReadOnlyCollection<IOperationCommand<CreateSequenceOperation, MigrationCommand>> Get()
        {
            return _modelExtractor.CreateSequenceIntents;
        }
    }

    internal class CreateSequenceSqlGenActionProvider : ISqlGenActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly ISqlFileInfoFactory _fileInfoProvider;
        private readonly ICreateSequenceOperationsProvider _commandProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public CreateSequenceSqlGenActionProvider(
            IServiceProvider provider,
            ISqlFileInfoFactory fileInfoProvider,
            ICreateSequenceOperationsProvider commandProvider,
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
                var tablePattern = _modelExtractor.DefaultSqlTargetOutput.SequencesPatternPath;
                var schema = cmd.Operation.Schema;
                var name = cmd.Operation.Name;
                var fi = _fileInfoProvider.CreateFileInfo(rootSqlPath, tablePattern, schema, name);

                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                                      .UseCommandText(cmd.Command.CommandText)
                                      .UseTargetFiles(fi)
                                      .Build();
            }


        }
    }




}
