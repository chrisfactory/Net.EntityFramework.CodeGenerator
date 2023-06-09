using Net.EntityFramework.CodeGenerator.Core;
using System.Text;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class InsertGenerator : ISqlContent
    {
        private readonly IEnumerable<IEntityColumn> _projectionColumns;
        private readonly IEnumerable<IEntityColumn> _outputColumns;
        private readonly string _from;
        public InsertGenerator(string from, IEnumerable<IEntityColumn> projectionColumns, IEnumerable<IEntityColumn> outputColumns)
        {
            _from = from;
            _projectionColumns = projectionColumns;
            _outputColumns = outputColumns;
        }
        public void Build(StringBuilder builder)
        {
            builder.AppendLine($"   INSERT INTO {_from}");
            BuildInsertColumns(builder, InsertSegements.Insert, _projectionColumns);
            builder.AppendLine($"   OUTPUT");
            BuildInsertColumns(builder, InsertSegements.Output, _outputColumns);
            builder.AppendLine($"   VALUES");
            BuildInsertColumns(builder, InsertSegements.Values, _projectionColumns);
        }


        private enum InsertSegements
        {
            Insert,
            Output,
            Values
        }

        private static void BuildInsertColumns(StringBuilder builder, InsertSegements segementType, IEnumerable<IEntityColumn> keys)
        {
            var start = new Column(6, ColumnAlignment.Right);
            var name = new Column();
            var sep = new SeparatorColumn(",");
            var requ = new DataFormater(start, name, sep);
            var closable = true;
            string columnPrefix = string.Empty;
            string at = string.Empty;
            string cs = string.Empty;
            string ce = string.Empty;   
            switch (segementType)
            {
                case InsertSegements.Insert:
                    cs = "[";
                    ce = "]";
                    break;
                case InsertSegements.Output:
                    closable = false;
                    columnPrefix = "[INSERTED].";
                    cs = "[";
                    ce = "]";
                    break;
                case InsertSegements.Values:
                    at = "@";
                    break; 
            }

            string startStr = closable ? "(" : string.Empty;
            var last = keys.Last();
            foreach (var column in keys)
            {
                var end = (closable && column == last) ? ")" : null;
                requ.AddRow(startStr, $"{columnPrefix}{cs}{at}{column.ColumnName}{ce}{end}", null);
                startStr = string.Empty;
            }

            var result = requ.Build();

            foreach (var item in result.Lines)
            {
                builder.AppendLine(item);
            }
        }
    }
}
