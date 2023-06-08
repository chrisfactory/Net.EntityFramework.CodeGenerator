using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpSelectSource 
    {
        public SpSelectSource(
            IDbContextModelContext context,
            IResultSet resultSet,
            IMutableEntityType entity,
            //IStoredProcedureNameProvider storedProcedureNameProvider,
            IStoredProcedureSchemaProvider storedProcedureSchemaProvider)
        {

            DbContextType = context.DbContextType;
            IsSelfDbContext = context.IsSelfDbContext;
            ResultSet = resultSet.ResultSet;

            TableName = entity.GetTableName();
            TableFullName = entity.GetTableFullName();
            EntityTable = context.Entities.Single(e => e.TableFullName == TableFullName);
            ProjectionColumns = EntityTable.AllColumns.ToList();
            PrimaryKeys = EntityTable.PrimaryKeys.ToList();

            Schema = storedProcedureSchemaProvider.Get();

            //var action = string.Empty;
            //switch (ResultSet)
            //{
            //    case ResultSets.None:
            //        action = "Select";
            //        break;
            //    case ResultSets.First: 
            //    case ResultSets.FirstOrDefault: 
            //    case ResultSets.Single: 
            //    case ResultSets.SingleOrDefault:
            //        action = "Get";
            //        break;
            //    default:
            //    throw new NotImplementedException($"{ResultSet}");
            //}

            //Name = storedProcedureNameProvider.Get(new SPInfos()
            //{
            //    TableName = TableName,
            //    Parameters = PrimaryKeys,
            //    Action = "Select"
            //});
        }

        public Type DbContextType { get; }
        public bool IsSelfDbContext { get; }
        public ResultSets ResultSet { get; }
        public string? Schema { get; }
        public string Name { get; }
        public string TableName { get; }
        public string TableFullName { get; }
        public IEntityTypeTable EntityTable { get; }
        public IReadOnlyCollection<IEntityColumn> ProjectionColumns { get; }
        public IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; }


    }
}
