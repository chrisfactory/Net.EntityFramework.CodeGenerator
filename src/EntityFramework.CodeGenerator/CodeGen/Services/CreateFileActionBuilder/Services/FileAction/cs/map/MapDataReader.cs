using System.Data;
using System.Text;

namespace EntityFramework.CodeGenerator
{
    internal class MapDataReader : IContentFileSegment
    {
        private readonly IEntityTypeTable _entity;
        public MapDataReader(IEntityTypeTable entity)
        {
            _entity = entity;
        }


        public void Build(StringBuilder builder)
        {
            var clrType = _entity.EntityType.ClrType;
            string typeName = clrType.Name;

            var columns = _entity.PrimaryKeys.Concat(_entity.UpdatableColumns).ToList();
         
            builder.AppendLine($"    public static {typeName} Map(this {typeName} data, {nameof(IDataRecord)} dataRecord)");
            builder.AppendLine("    {");

            foreach (var column in columns)
                builder.AppendLine($"        data.{column.PropertyName} = dataRecord.Get<{column.PropertyType}>(\"{column.ColumnName}\");");

            builder.AppendLine($"        return data;");
            builder.AppendLine("    }");

        }
    }
}
