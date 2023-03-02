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



        public IEnumerable<IAction> GetOperations()
        {
         
            var providers = new List<IActionProvider>();
            var firstLevelBuilder = new List<IActionBuilder>();
            var secondLevelBuilder = new List<IActionBuilder>();

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
                var builders = new List<IActionBuilder>();
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
        }
    }
}
