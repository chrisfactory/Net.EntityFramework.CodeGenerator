using System.Data;
using System.Text;

namespace SqlG
{
    internal class MapParameters : IContentFileSegment
    {
        private readonly IEntityTypeTable _entity;
        public MapParameters(IEntityTypeTable entity)
        {
            _entity = entity;
        }


        public void Build(StringBuilder builder)
        {
            var clrType = _entity.EntityType.ClrType;
            string typeName = clrType.Name;

            var columns = _entity.PrimaryKeys.Concat(_entity.UpdatableColumns).ToList();
            var lastColumn = columns.Last();


            builder.AppendLine($"    public static IReadOnlyDictionary<string, object?> Map(this {typeName} data)");
            builder.AppendLine("    {");
            builder.AppendLine("        var result = new Dictionary<string, object?>");
            builder.AppendLine("        {");

            foreach (var column in columns)
            {
                string end = column != lastColumn ? "," : "";
                builder.AppendLine($"            {{ \"@{column.ColumnName}\", data.{column.PropertyName} }}{end}");

            }
            builder.AppendLine("        };");
            builder.AppendLine($"        return result;");
            builder.AppendLine("    }");

        }
    }
}
