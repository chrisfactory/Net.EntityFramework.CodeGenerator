using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal class SqlGBuilder : ISqlGBuilder
    {
        public SqlGBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<IProjectResolver, CurrentCSProjectResolver>();
            Services.AddSingleton<IFilePathProvider, FilePathProvider>();
        }
        public IServiceCollection Services { get; }

        public ICodeGenerator Build()
        {  
            Services.AddSingleton<ICodeGenerator, CodeGenerator>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ICodeGenerator>();
        }
    }


    public interface ICodeGenerator
    {

    }

    internal class CodeGenerator : ICodeGenerator
    {
        public CodeGenerator(DbContext dbContext , IFilePathProvider filePathProvider)
        {

            var designTimeMode = dbContext.GetService<IDesignTimeModel>();
            var model = designTimeMode.Model;
            var rm = model.GetRelationalModel();

            var tables = rm.Tables.ToList().ToDictionary(t => t.Name);
            var Model = rm.Model;

            var sequences = rm.Sequences.ToList();
            var Queries = rm.Queries.ToList();
            var StoredProc = rm.StoredProcedures.ToList();
            var views = rm.Views.ToList();
            var Functions = rm.Functions.ToList();
            var Collation = rm.Collation;


            foreach (var item in Model.GetEntityTypes())
            {
                var tableName = item.GetTableName();
                var table = tables[tableName];

                var tpping = item.GetTableMappings().ToList();
              
            }
            //var diff = f.GetService<IMigrationsModelDiffer>();
            //var cmds = diff.GetDifferences(null, rm);
            //var mig = f.GetService<IMigrationsSqlGenerator>();
            //// var mig = f.GetService<imigr>();

        }

    }

    internal class EntityTypeTable
    {
        public EntityTypeTable(IModel model, IEntityType entityType, ITable table)
        {
            Model = model;
            EntityType = entityType;
            Table = table;
        }
        public IModel Model { get; }
        public IEntityType EntityType { get; }
        public ITable Table { get; }

    }
}
