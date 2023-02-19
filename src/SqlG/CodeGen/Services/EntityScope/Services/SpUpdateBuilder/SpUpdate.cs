using System.Text;

namespace SqlG
{
    internal class SpUpdate : IContentFileSegment
    {
        private readonly IEntityTypeTable _entity;
        private readonly string _spName;
        public SpUpdate(IEntityTypeTable entity, string spName)
        {
            _entity = entity;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {

            string relatedSchemaExt = string.IsNullOrWhiteSpace(_entity.Table.Schema) ? "" : $"[{_entity.Table.Schema}].";

            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");
            var lastColumn = _entity.AllColumns.Last();
            foreach (var param in _entity.AllColumns)
            { 
                string end = param != lastColumn ? "," : "";
                builder.AppendLine($"     @{param.ColumnName} {param.SqlType?.ToUpper()}{end}");
            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");
            builder.AppendLine($"    UPDATE {relatedSchemaExt}[{_entity.Table.Name}]");
            builder.AppendLine($"    SET");

            var lastUpdt = _entity.UpdatableColumns.Last();
            foreach (var param in _entity.UpdatableColumns)
            { 
                string end = param != lastUpdt ? "," : "";
                builder.AppendLine($"        [{param.ColumnName}] = @{param.ColumnName}{end}");
            }
            builder.AppendLine($"    WHERE");

            var firstPk = _entity.PrimaryKeys.First();
            foreach (var param in _entity.PrimaryKeys)
            {
                string and = firstPk != param ? "AND " : "    " ;
                builder.AppendLine($"        {and}[{param.ColumnName}] = @{param.ColumnName}"); 
            }
            builder.AppendLine("");
            builder.AppendLine("END");


        }

    }
}
