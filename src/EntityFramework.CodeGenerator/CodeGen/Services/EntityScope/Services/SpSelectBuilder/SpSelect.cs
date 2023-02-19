using System.Text;

namespace EntityFramework.CodeGenerator
{
    internal class SpSelect : IContentFileSegment
    {
        private readonly IEntityTypeTable _entity;
        private readonly string _spName;
        public SpSelect(IEntityTypeTable entity, string spName)
        {
            _entity = entity;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {
            string relatedSchemaExt = string.IsNullOrWhiteSpace(_entity.Table.Schema) ? "" : $"[{_entity.Table.Schema}].";

            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");
            var lastPk = _entity.PrimaryKeys.Last();
            foreach (var param in _entity.PrimaryKeys)
            {
                string end = param != lastPk ? "," : "";
                builder.AppendLine($"     @{param.ColumnName} {param.SqlType.ToUpper()}{end}");

            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");
            builder.AppendLine($"    SELECT DATA_RESULT.* FROM {relatedSchemaExt}[{_entity.Table.Name}] AS DATA_RESULT");
            builder.AppendLine($"    WHERE");
            var first = _entity.PrimaryKeys.First();
            foreach (var param in _entity.PrimaryKeys)
            {
                string and = param != first ? "AND " : "    ";
                builder.AppendLine($"        {and}[{param.ColumnName}] = @{param.ColumnName}");
            }
            builder.AppendLine("");
            builder.AppendLine("END");


        }
    }
}
