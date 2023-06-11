using Net.EntityFramework.CodeGenerator.Core;
using System.Text;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DeleteGenerator : ISqlContent
    {
        private readonly IEnumerable<IEntityColumn> _outputColumns;
        private readonly IEnumerable<IEntityColumn> _columnsConstraint;
        private readonly string _from; 
        public DeleteGenerator(string from, IEnumerable<IEntityColumn> outputColumns, IEnumerable<IEntityColumn> columnsConstraint)
        {
            _from = from;
            _outputColumns = outputColumns;
            _columnsConstraint = columnsConstraint;
        }
        public void Build(StringBuilder builder)
        { 
            builder.AppendLine($"   DELETE {_from}"); 
            builder.AppendLine($"   OUTPUT");
            BuildOutputColumns(builder, _outputColumns);
            builder.AppendLine($"   WHERE ");
            BuildWhere(builder, _columnsConstraint);
        }

        private static void BuildOutputColumns(StringBuilder builder, IEnumerable<IEntityColumn> keys)
        {
            var start = new Column(6, ColumnAlignment.Right);
            var name = new Column();
            var sep = new SeparatorColumn(",");
            var requ = new DataFormater(start, name, sep);
            var closable = false;
            string columnPrefix = "[DELETED].";
            string at = string.Empty;
            string cs = "[";
            string ce = "]";


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


     
        private void BuildWhere(StringBuilder builder, IEnumerable<IEntityColumn> keys)
        {
            var columnTable = new Column(6, ColumnAlignment.Left);
            var equal = new ConstanteColumn("=", 1, ColumnAlignment.Center, 1);
            var argColumnTableTable = new Column(ColumnAlignment.Left);
            var sep = new SeparatorColumn("AND,", 2);
            var requ = new DataFormater(columnTable, equal, argColumnTableTable, sep);

            foreach (var column in keys)
            {
                requ.AddRow($"[{column.ColumnName}]", null, $"@{column.ColumnName}", null);
            }

            var result = requ.Build();

            foreach (var item in result.Lines)
            {
                builder.AppendLine(item);
            }
        }
    }
}
