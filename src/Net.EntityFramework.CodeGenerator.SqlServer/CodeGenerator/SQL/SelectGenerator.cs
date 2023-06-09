using Net.EntityFramework.CodeGenerator.Core;
using System.Text;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SelectGenerator : ISqlContent
    {
        private readonly IEnumerable<IEntityColumn> _projectionColumns;
        private readonly IEnumerable<IEntityColumn> _columnsConstraint;
        private readonly string _from;
        private readonly bool _nolock;
        public SelectGenerator(string from, bool nolock, IEnumerable<IEntityColumn> projectionColumns, IEnumerable<IEntityColumn> columnsConstraint)
        {
            _from = from;
            _nolock = nolock;
            _projectionColumns = projectionColumns;
            _columnsConstraint = columnsConstraint;
        }
        public void Build(StringBuilder builder)
        {
            string nolock = _nolock ? " With(Nolock)" : string.Empty;
            builder.AppendLine($"   SELECT ");
            BuildProjection(builder, _projectionColumns);
            builder.AppendLine($"   FROM {_from} {nolock}");
            builder.AppendLine($"   WHERE ");
            BuildWhere(builder, _columnsConstraint);
        }




        private static void BuildProjection(StringBuilder builder, IEnumerable<IEntityColumn> keys)
        {
            var name = new Column(6);
            var sep = new SeparatorColumn(",");
            var requ = new DataFormater(name, sep);

            foreach (var column in keys)
            {
                requ.AddRow($"[{column.ColumnName}]", null);
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
            var equal = new ConstanteColumn("=", 2, ColumnAlignment.Center, 2);
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
