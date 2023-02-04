using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections;
using System.Text;

namespace SqlG
{
    internal class SpDelete : IContentFileRootSegment
    {
        private readonly CreateTableOperation _operation;
        private readonly string _spName;
        public SpDelete(CreateTableOperation operation, string spName)
        {
            _operation = operation;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {


            string relatedSchemaExt = string.IsNullOrWhiteSpace(_operation.Schema) ? "" : $"[{_operation.Schema}].";
             
            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");
            var cCount = _operation.PrimaryKey.Columns.Length;
            var i = cCount;
            foreach (var param in _operation.PrimaryKey.Columns)
            {
                i--;
                var c = _operation.Columns.Single(c => c.Name == param);
                string end = (i > 0) ? "," : "";
                builder.AppendLine($"     @{param} {c.ColumnType?.ToUpper()}{end}");

            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");
            builder.AppendLine($"    DELETE FROM {relatedSchemaExt}[{_operation.Name}]");
            builder.AppendLine($"    WHERE");
            i = cCount;
            foreach (var param in _operation.PrimaryKey.Columns)
            {
                string and = cCount > 1 && i != cCount ? "AND " : (cCount > 1 ? "    " : string.Empty);
                builder.AppendLine($"        {and}[{param}] = @{param}");
                i--;
            }
            builder.AppendLine("");
            builder.AppendLine("END");


        }

        public IEnumerator<IContentFileSegment> GetEnumerator()
        {
            return Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }
    }
}
