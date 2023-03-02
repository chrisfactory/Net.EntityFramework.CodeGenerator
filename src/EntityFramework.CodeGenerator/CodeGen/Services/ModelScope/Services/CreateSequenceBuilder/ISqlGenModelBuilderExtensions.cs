using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder Sequences(this ISqlGenModelBuilder genBuilder)
        {
            return genBuilder.AddGenActionBuilder<ICreateSequenceBuilder, CreateSequenceBuilder>();
        }
    }




    public interface ICreateSequenceBuilder : IActionBuilder
    {
    }

    internal class CreateSequenceBuilder : ActionBuilder, ICreateSequenceBuilder
    {

        public override IActionProvider Build()
        {
            Services.AddSingleton<ICreateSequenceOperationsProvider, CreateSequenceOperationsProvider>();
            Services.AddSingleton<IActionProvider, CreateSequenceSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
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

    internal class CreateSequenceSqlGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly ICreateSequenceOperationsProvider _commandProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public CreateSequenceSqlGenActionProvider(
            IServiceProvider provider,
            IFileInfoFactory fileInfoProvider,
            ICreateSequenceOperationsProvider commandProvider,
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
                var tablePattern = _modelExtractor.DefaultSqlTargetOutput.SequencesPatternPath;
                var schema = cmd.Operation.Schema;
                var name = cmd.Operation.Name;
                var fi = _fileInfoProvider.CreateSqlFileInfo(rootSqlPath, tablePattern, schema, name);

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
