using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

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



        public IEnumerable<ISqlGenAction> GetOperations()
        {
            var rootSqlPath = _ModelExtractor.DefaultSqlTargetOutput.RootPath;
            var rootCsPath = _ModelExtractor.DefaultCsTargetOutput.RootPath;

            var providers = new List<ISqlGenActionProvider>();
            var firstLevelBuilder = new List<ISqlGenActionBuilder>();
            var secondLevelBuilder = new List<ISqlGenActionBuilder>();

            ICreateTablesSqlGenActionProvider? tablesBuilderProvider = null;
            ICreateIndexSqlGenActionProvider? indexsBuilderProvider = null;
            foreach (var modelBuilder in _ModelExtractor.ActionBuilders)
            {
                modelBuilder.Services.AddSingleton(_ModelExtractor);

                if (modelBuilder is ICreateTablesBuilder createTablesBuilder && createTablesBuilder.Build() is ICreateTablesSqlGenActionProvider modeltableProvider)
                {
                    tablesBuilderProvider = modeltableProvider;
                    providers.Add(modeltableProvider);
                }
                else if (modelBuilder is ICreateIndexsBuilder createIndexsBuilder && createIndexsBuilder.Build() is ICreateIndexSqlGenActionProvider modelIndexProvider)
                {
                    indexsBuilderProvider = modelIndexProvider;
                    providers.Add(indexsBuilderProvider);
                }
                else
                    secondLevelBuilder.Add(modelBuilder);
            }


            foreach (var entity in _ModelExtractor.Entities)
            {
                bool table = false;
                bool index = false;
                var builders = new List<ISqlGenActionBuilder>();
                foreach (var builder in entity.ActionBuilders)
                {
                    builders.Add(builder);

                    if (builder is ICreateTableBuilder createTableBuilder)
                        table = true;
                    else if (builder is ICreateIndexBuilder createIndexBuilder)
                        index = true;
                }

                if (!table && tablesBuilderProvider != null)
                    builders.Add(tablesBuilderProvider.GetTableBuilder());
                if (!index && indexsBuilderProvider != null)
                    builders.Add(indexsBuilderProvider.GetIndexBuilder());


                foreach (var builder in builders)
                {
                    builder.Services.AddSingleton(entity);
                    builder.Services.AddSingleton(_ModelExtractor);


                    bool secondLevel = false;

                    if (secondLevel)
                        secondLevelBuilder.Add(builder);
                    else
                        firstLevelBuilder.Add(builder);
                }  
            }


            foreach (var builder in firstLevelBuilder)
                providers.Add(builder.Build());
            foreach (var builder in secondLevelBuilder)
                providers.Add(builder.Build());

            foreach (var provider in providers)
                foreach (var action in provider.Get())
                    yield return action;



            foreach (var item in _ModelExtractor.CreateTableIntents)
            {

                var entity = _ModelExtractor.Entities.Where(e => e.EntityType.GetTableName() == item.Operation.Name).SingleOrDefault();
 

                // var spSelect = $"Select{item.Operation.Name}";
                // yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                //   .UseFileAction(new SpSelect(item.Operation, spSelect))
                //   .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spSelect))
                //   .Build();


                // var spUpdate = $"Update{item.Operation.Name}";
                // yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                //   .UseFileAction(new SpUpdate(item.Operation, spUpdate))
                //   .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spUpdate))
                //   .Build();

                // var spDelete = $"Delete{item.Operation.Name}";
                // yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                //   .UseFileAction(new SpDelete(spDelete, entity))
                //   .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spDelete))
                // .Build();


                // var mapDr = new MapDataReader(entity);
                // var mapParam = new MapParameters(entity);
                // string mapExtName = $"{item.Operation.Name}MapExtensions";
                // yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                //   .UseFileAction(new CsMapExt(mapExtName, entity, mapDr, mapParam))
                //   .UseTargetFiles(GetCsFileInfo(rootCsPath, _ModelExtractor.DefaultCsTargetOutput.MapExtensionsPatternPath, item.Operation.Schema, item.Operation.Name, mapExtName))
                // .Build();


                // var spInsert = $"Insert{item.Operation.Name}";
                // yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                //   .UseFileAction(new SpInsert(spInsert, entity))
                //   .UseTargetFiles(GetSqlFileInfo(rootSqlPath, _ModelExtractor.DefaultSqlTargetOutput.StoredProceduresPatternPath, item.Operation.Schema, item.Operation.Name, spInsert))
                //   .Build();

                // var insertMap = new CsCallInsertPs(spInsert, entity);
                // var selectMap = new CsCallSelectPs(spSelect, entity);
                // var updateMap = new CsCallUpdatetPs(spUpdate, entity);
                // var deleteMap = new CsCallDeletePs(spDelete, entity);
                // string className = $"{item.Operation.Name}DbService";
                // yield return _provider.GetRequiredService<ICreateFileActionBuilder>()
                //   .UseFileAction(new CsDbAccess(className, entity, insertMap, selectMap, updateMap, deleteMap))
                //   .UseTargetFiles(GetCsFileInfo(rootCsPath, _ModelExtractor.DefaultCsTargetOutput.DbServicePatternPath, item.Operation.Schema, item.Operation.Name, className))
                // .Build();


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


        private FileInfo GetCsFileInfo(string rootPath, string pattern, string? schema, string operationName, string classname)
        {
            var str = new StringFormatter(pattern);
            str.Add("{schema}", schema);
            str.Add("{name}", operationName);
            str.Add("{classname}", classname);            var subPath = str.ToString().TrimStart('\\');
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
