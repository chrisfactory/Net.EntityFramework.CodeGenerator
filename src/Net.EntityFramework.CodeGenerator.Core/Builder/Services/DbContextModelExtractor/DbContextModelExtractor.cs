﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class DbContextModelExtractor : IDbContextModelContext
    {
        private readonly DbContext _context;
        private readonly IRelationalModel _relationModel;
        public DbContextModelExtractor(DbContext dbContext)
        {

            _context = dbContext;
            IsSelfDbContext = _context is SelfDbContext;
            DbContextType = _context.GetType();

            var designTimeMode = dbContext.GetService<IDesignTimeModel>();
            var diff = dbContext.GetService<IMigrationsModelDiffer>();
            var mig = dbContext.GetService<IMigrationsSqlGenerator>();
            var model = designTimeMode.Model;
            _relationModel = model.GetRelationalModel();

            Model = model;



            var sqlTargetAnnotation = model.FindAnnotation(DataProjectOptions.AnnotationKey);
            DataProjectTargetInfos = sqlTargetAnnotation?.Value as DataProjectOptions ?? new DataProjectOptions();

            var csTargetAnnotation = Model.FindAnnotation(DotNetProjectOptions.AnnotationKey);
            DotNetProjectTargetInfos = csTargetAnnotation?.Value as DotNetProjectOptions ?? new DotNetProjectOptions();


            var operations = diff.GetDifferences(null, _relationModel);
            List<IOperationCommand<CreateTableOperation, MigrationCommand>> createTables;
            List<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> schemaTables;
            List<IOperationCommand<CreateIndexOperation, MigrationCommand>> indexsTables;
            List<IOperationCommand<CreateSequenceOperation, MigrationCommand>> sequenceTables;
            ExtractEfIntents(operations, mig, out createTables, out schemaTables, out indexsTables, out sequenceTables);

            CreateTableIntents = createTables;
            CreateIndexIntents = indexsTables;
            EnsureSchemaIntents = schemaTables;
            CreateSequenceIntents = sequenceTables;

            Entities = ExtractEntities();

        }


        public Type DbContextType { get; }
        public bool IsSelfDbContext { get; }


        public IModel Model { get; }
        public IDotNetProjectTargetInfos DotNetProjectTargetInfos { get; }
        public IDataProjectTargetInfos DataProjectTargetInfos { get; }

        public IReadOnlyCollection<IEntityTypeTable> Entities { get; }


        public IReadOnlyCollection<IOperationCommand<CreateTableOperation, MigrationCommand>> CreateTableIntents { get; }
        public IReadOnlyCollection<IOperationCommand<CreateIndexOperation, MigrationCommand>> CreateIndexIntents { get; }
        public IReadOnlyCollection<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> EnsureSchemaIntents { get; }
        public IReadOnlyCollection<IOperationCommand<CreateSequenceOperation, MigrationCommand>> CreateSequenceIntents { get; }

        private IReadOnlyCollection<IEntityTypeTable> ExtractEntities()
        {
            var result = new List<IEntityTypeTable>();

            var tables = _relationModel.Tables.ToDictionary(t => t.Name);

            foreach (var entityType in Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!string.IsNullOrEmpty(tableName))
                {
                    var table = tables[tableName];
                    result.Add(new EntityTypeTable(Model, entityType, table));

                }
            }
            return result;
        }
        public IEntityTypeTable GetEntity(IMutableEntityType mutableEntity)
        { 
            var tableFullName = mutableEntity.GetTableFullName();
            return Entities.Single(e => e.TableFullName == tableFullName && e.EntityType.ClrType == mutableEntity.ClrType);
        }

        private void ExtractEfIntents(IReadOnlyList<MigrationOperation> operations, IMigrationsSqlGenerator mig, out List<IOperationCommand<CreateTableOperation, MigrationCommand>> createTables, out List<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> schemaTables, out List<IOperationCommand<CreateIndexOperation, MigrationCommand>> indexsTables, out List<IOperationCommand<CreateSequenceOperation, MigrationCommand>> sequenceTables)
        {
            createTables = new List<IOperationCommand<CreateTableOperation, MigrationCommand>>();
            schemaTables = new List<IOperationCommand<EnsureSchemaOperation, MigrationCommand>>();
            indexsTables = new List<IOperationCommand<CreateIndexOperation, MigrationCommand>>();
            sequenceTables = new List<IOperationCommand<CreateSequenceOperation, MigrationCommand>>();
            foreach (var operation in operations)
            {

                if (operation is CreateTableOperation co)
                {
                    foreach (var cmd in mig.Generate(new List<MigrationOperation> { operation }))
                        createTables.Add(new OperationCommand<CreateTableOperation, MigrationCommand>(co, cmd));
                }
                else if (operation is EnsureSchemaOperation sch)
                {
                    foreach (var cmd in mig.Generate(new List<MigrationOperation> { operation }))
                        schemaTables.Add(new OperationCommand<EnsureSchemaOperation, MigrationCommand>(sch, cmd));
                }
                else if (operation is CreateIndexOperation idx)
                {
                    foreach (var cmd in mig.Generate(new List<MigrationOperation> { operation }))
                        indexsTables.Add(new OperationCommand<CreateIndexOperation, MigrationCommand>(idx, cmd));
                }
                else if (operation is CreateSequenceOperation seq)
                {
                    foreach (var cmd in mig.Generate(new List<MigrationOperation> { operation }))
                        sequenceTables.Add(new OperationCommand<CreateSequenceOperation, MigrationCommand>(seq, cmd));
                }
                else
                {

                }
            }
        }
    }
}
