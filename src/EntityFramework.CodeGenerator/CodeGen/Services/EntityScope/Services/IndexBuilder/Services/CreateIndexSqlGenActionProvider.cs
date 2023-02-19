using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    internal class CreateIndexSqlGenActionProvider : ISqlGenActionProvider
    {
        private readonly IServiceProvider _provider;
        private readonly ISqlFileInfoFactory _fileInfoProvider;
        private readonly ICreateIndexOperationsProvider _commandProvider;
        private readonly IDbContextModelExtractor _modelExtractor;
        public CreateIndexSqlGenActionProvider(
            IServiceProvider provider,
            ISqlFileInfoFactory fileInfoProvider,
            ICreateIndexOperationsProvider commandProvider,
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
                var tablePattern = _modelExtractor.DefaultSqlTargetOutput.IndexsPatternPath;
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
