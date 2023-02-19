using System.Text;

namespace SqlG
{
    internal class SpInsert : IContentFileSegment
    {
        private readonly IEntityTypeTable _entity;
        private readonly string _spName;
        public SpInsert(IEntityTypeTable entity, string spName)
        {
            _entity = entity;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {


            string relatedSchemaExt = string.IsNullOrWhiteSpace(_entity.Table.Schema) ? "" : $"[{_entity.Table.Schema}].";



            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");

            var lastColumn = _entity.UpdatableColumns.Last();

            foreach (var param in _entity.UpdatableColumns)
            {
                string end = lastColumn != param ? "," : "";
                builder.AppendLine($"     @{param.ColumnName} {param.SqlType?.ToUpper()}{end}");

            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");




            builder.AppendLine("DECLARE @OutputTable TABLE");
            builder.AppendLine("(");
            var l1 = _entity.AllColumns.Last();
            foreach (var param in _entity.AllColumns)
            {
                string end = l1 != param ? "," : "";
                builder.AppendLine($"     [{param.ColumnName}] {param.SqlType?.ToUpper()}{end}");

            }
            builder.AppendLine(")");

            builder.AppendLine();
            builder.AppendLine();




            builder.AppendLine($"    INSERT INTO {relatedSchemaExt}[{_entity.Table.Name}]");
            builder.AppendLine("                 (");

            foreach (var param in _entity.UpdatableColumns)
            {
                string end = lastColumn != param ? "," : "";
                builder.AppendLine($"                    [{param.ColumnName}]{end}");

            }
            builder.AppendLine("                 )");

            var strColumns = string.Empty;
            foreach (var param in _entity.AllColumns)
            {
                string end = l1 != param ? "," : "";
                strColumns += $"INSERTED.[{param.ColumnName}]{end} ";

            }

            builder.AppendLine($"         OUTPUT {strColumns} INTO @OutputTable");


            builder.AppendLine("         VALUES");
            builder.AppendLine("                 (");

            foreach (var param in _entity.UpdatableColumns)
            {
                string end = lastColumn != param ? "," : "";
                builder.AppendLine($"                    @{param.ColumnName}{end}");

            }
            builder.AppendLine("                 )");
            builder.AppendLine("");

            builder.AppendLine("SELECT * FROM @OutputTable");
            builder.AppendLine("END");

        }
    }
}
