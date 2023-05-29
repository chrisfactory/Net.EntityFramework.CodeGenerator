using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpSelectSource : ISpSelectCodeGeneratorSource
    { 
        public SpSelectSource(
            IDbContextModelExtractor context,
            IMutableEntityType entity,
            IStoredProcedureNameProvider storedProcedureNameProvider,
            IStoredProcedureSchemaProvider storedProcedureSchemaProvider)
        {
             
            Schema = storedProcedureSchemaProvider.Get();
            Name = storedProcedureNameProvider.Get();
            TableName = entity.GetTableName();
            TableFullName = entity.GetTableFullName();
            EntityTable = context.Entities.Single(e => e.TableFullName == TableFullName);
            ProjectionColumns = EntityTable.AllColumns.ToList();
            PrimaryKeys = EntityTable.PrimaryKeys.ToList();
        }
        public string? Schema { get; }
        public string Name { get; }
        public string TableName { get; }
        public string TableFullName { get; }
        public IEntityTypeTable EntityTable { get; }
        public IReadOnlyCollection<IEntityColumn> ProjectionColumns { get; }
        public IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; }

    }
}
