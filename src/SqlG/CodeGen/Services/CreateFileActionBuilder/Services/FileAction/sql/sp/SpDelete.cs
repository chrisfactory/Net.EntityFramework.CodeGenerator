using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections;
using System.Text;

namespace SqlG
{
    internal class SpDelete : IContentFileSegment
    {
        private readonly string _spName;
        private readonly IEntityTypeTable _entity;
        public SpDelete(string spName, IEntityTypeTable entity)
        {
            _entity = entity;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {

            if (_entity.PrimaryKeys.Count == 0)
                return;

            var firstKp = _entity.PrimaryKeys.First();
            var lastPk = _entity.PrimaryKeys.Last();

            string relatedSchemaExt = string.IsNullOrWhiteSpace(_entity.Table.Schema) ? "" : $"[{_entity.Table.Schema}].";

            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");

            foreach (var column in _entity.PrimaryKeys)
            {
                string end = column != lastPk ? "," : "";
                builder.AppendLine($"     @{column.ColumnName} {column.SqlType.ToUpper()}{end}");

            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");
            builder.AppendLine($"    DELETE FROM {relatedSchemaExt}[{_entity.Table.Name}]");
            builder.AppendLine($"    WHERE");

            foreach (var param in _entity.PrimaryKeys)
            {
                string and = param != firstKp ? "AND " : (_entity.PrimaryKeys.Count > 1 ? "    " : string.Empty);
                builder.AppendLine($"        {and}[{param.ColumnName}] = @{param.ColumnName}");
            }
            builder.AppendLine("");
            builder.AppendLine("END");


        }

    }
}
