using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Text;

namespace SqlG
{
    internal class SpInsert : IContentFileSegment
    {
        private readonly CreateTableOperation _operation;
        private readonly string _spName;
        public SpInsert(CreateTableOperation operation, string spName)
        {
            _operation = operation;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {


            string relatedSchemaExt = string.IsNullOrWhiteSpace(_operation.Schema) ? "" : $"[{_operation.Schema}].";

            var includePK = _operation.PrimaryKey?.Columns.Length > 1;
            var buildColumns = _operation.Columns.ToList();
            if (!includePK && _operation.PrimaryKey != null)
                buildColumns.RemoveAll(c => _operation.PrimaryKey.Columns.Contains(c.Name));


            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");

            var cCount = buildColumns.Count;
            var i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                builder.AppendLine($"     @{param.Name} {param.ColumnType?.ToUpper()}{end}");

            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");
            builder.AppendLine($"    INSERT INTO {relatedSchemaExt}[{_operation.Name}]");
            builder.AppendLine("                 (");
            i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                builder.AppendLine($"                    [{param.Name}]{end}");

            }
            builder.AppendLine("                 )");
            builder.AppendLine("         VALUES");
            builder.AppendLine("                 (");
            i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                builder.AppendLine($"                    @{param.Name}{end}");

            }
            builder.AppendLine("                 )");
            builder.AppendLine("");
            builder.AppendLine("END");

        }
    }
}
