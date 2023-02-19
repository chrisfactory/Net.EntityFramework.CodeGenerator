using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    internal class SqlGOperationsProvider : ISqlGOperationsProvider
    {
        private readonly IDbContextModelExtractor _ModelExtractor;
        public SqlGOperationsProvider(IDbContextModelExtractor model)
        {
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

        private FileInfo GetCsFileInfo(string rootPath, string pattern, string? schema, string operationName, string classname)
        {
            var str = new StringFormatter(pattern);
            str.Add("{schema}", schema);
            str.Add("{name}", operationName);
            str.Add("{classname}", classname); var subPath = str.ToString().TrimStart('\\');
            return new FileInfo(Path.Combine(rootPath, subPath));
        }
    }
}
