using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SqlG
{
    internal class SqlGOperationsProvider : ISqlGOperationsProvider
    {
        private readonly IServiceProvider _provider;
        private readonly IDbContextModelExtractor _ModelExtractor;
        public SqlGOperationsProvider(IServiceProvider provider, IDbContextModelExtractor model)
        {
            _provider = provider;
            _ModelExtractor = model;
            GetOperations();
        }



        public IEnumerable<ISqlGAction> GetOperations()
        {
            var rootSqlPath = _ModelExtractor.DefaultSqlTargetOutput.RootPath;
            foreach (var item in _ModelExtractor.CreateTableIntents)
            {
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                    .UseCommandText(item.Command.CommandText)
                    .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.TablesPatternPath, item.Operation.Schema, item.Operation.Name))
                    .Build();

                var spInsert = $"Insert{item.Operation.Name}";
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                  .UseFileAction(new SpInsert(item.Operation, spInsert))
                  .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spInsert))
                  .Build();

                var spUpdate = $"Update{item.Operation.Name}";
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                  .UseFileAction(new SpUpdate(item.Operation, spUpdate))
                  .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spUpdate))
                  .Build();

                var spDelete = $"Delete{item.Operation.Name}";
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                  .UseFileAction(new SpDelete(item.Operation, spDelete))
                  .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spDelete))
                  .Build();

            }
            foreach (var item in _ModelExtractor.CreateIndexIntents)
            {
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                    .UseCommandText(item.Command.CommandText)
                    .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.IndexsPatternPath, item.Operation.Schema, item.Operation.Name))
                    .Build();
            }
            foreach (var item in _ModelExtractor.CreateSequenceIntents)
            {
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                    .UseCommandText(item.Command.CommandText)
                    .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.SequencesPatternPath, item.Operation.Schema, item.Operation.Name))
                    .Build();
            }
            foreach (var item in _ModelExtractor.EnsureSchemantents)
            {
                yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                    .UseCommandText(item.Command.CommandText)
                    .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.SchemasPatternPath, string.Empty, item.Operation.Name))
                    .Build();
            }
        }

        private FileInfo GetSqlFileInfo(string rootPath, string pattern, string? schema, string operationName, string spName = null)
        {
            var str = new StringFormatter(pattern);
            str.Add("{schema}", schema);
            str.Add("{schemaExt}", string.IsNullOrWhiteSpace(schema) ? "" : $"{schema}.");
            str.Add("{name}", operationName);
            str.Add("{spname}", spName);
            var subPath = str.ToString().TrimStart('\\');
            return new FileInfo(Path.Combine(rootPath, subPath));
        }
    }
    internal class StringFormatter
    {

        public string Str { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public StringFormatter(string p_str)
        {
            Str = p_str;
            Parameters = new Dictionary<string, object>();
        }

        public void Add(string key, object val)
        {
            Parameters.Add(key, val);
        }

        public override string ToString()
        {
            return Parameters.Aggregate(Str, (current, parameter) => current.Replace(parameter.Key, parameter.Value?.ToString()));
        }

    }

}
