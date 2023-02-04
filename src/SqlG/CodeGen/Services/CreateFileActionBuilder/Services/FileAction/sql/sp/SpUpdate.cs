using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections;
using System.Text;

namespace SqlG
{
    internal class SpUpdate : IContentFileRootSegment
    {
        private readonly CreateTableOperation _operation;
        private readonly string _spName;
        public SpUpdate(CreateTableOperation operation, string spName)
        {
            _operation = operation;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {


            string relatedSchemaExt = string.IsNullOrWhiteSpace(_operation.Schema) ? "" : $"[{_operation.Schema}].";

            if (_operation.PrimaryKey == null)
                return;
            var buildColumns = _operation.Columns.ToList();

            buildColumns.RemoveAll(c => _operation.PrimaryKey.Columns.Contains(c.Name));

            if (buildColumns.Count == 0)
                return;
             
            builder.AppendLine($"CREATE PROCEDURE {relatedSchemaExt}[{_spName}]");
            builder.AppendLine("(");
            var i = _operation.Columns.Count;
            foreach (var param in _operation.Columns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                builder.AppendLine($"     @{param.Name} {param.ColumnType?.ToUpper()}{end}");
            }
            builder.AppendLine(")");
            builder.AppendLine("AS BEGIN");
            builder.AppendLine("");
            builder.AppendLine($"    UPDATE {relatedSchemaExt}[{_operation.Name}]");
            builder.AppendLine($"    SET");


            if (_operation.PrimaryKey != null)
                buildColumns.RemoveAll(c => _operation.PrimaryKey.Columns.Contains(c.Name));
            var cCount = buildColumns.Count;
            i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                builder.AppendLine($"        [{param.Name}] = @{param.Name}{end}");
            }
            builder.AppendLine($"    WHERE");

            cCount = _operation.PrimaryKey.Columns.Length;
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
