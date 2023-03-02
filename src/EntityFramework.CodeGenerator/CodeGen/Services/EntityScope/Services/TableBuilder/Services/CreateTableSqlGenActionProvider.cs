using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    internal class CreateTableSqlGenActionProvider : IActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IFileInfoFactory _fileInfoProvider;
        private readonly ICreateTableOperationProvider _commandProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public CreateTableSqlGenActionProvider(
            IServiceProvider provider,
            IFileInfoFactory fileInfoProvider,
            ICreateTableOperationProvider commandProvider,
            IDbContextModelExtractor modelExtractor)
        {
            _provider = provider;
            _fileInfoProvider = fileInfoProvider;
            _commandProvider = commandProvider;
            _modelExtractor = modelExtractor;
        }
        public IEnumerable<IAction> Get()
        {
            var cmd = _commandProvider.Get();

            var rootSqlPath = _modelExtractor.DefaultSqlTargetOutput.RootPath;
            var tablePattern = _modelExtractor.DefaultSqlTargetOutput.TablesPatternPath;
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
