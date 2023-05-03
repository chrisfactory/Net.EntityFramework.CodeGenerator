using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SqlSpSelectPackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        private readonly IStoredProcedureNameProvider storedProcedureNameProvider;
        private readonly IStoredProcedureSchemaProvider storedProcedureSchemaProvider;
        public SqlSpSelectPackageContentProvider(IModule module, IPackageScope scope, ISpSelectCodeGeneratorSource source)
        {
            storedProcedureNameProvider = module.Provider.GetRequiredService<IStoredProcedureNameProvider>();
            storedProcedureSchemaProvider = module.Provider.GetRequiredService<IStoredProcedureSchemaProvider>();

            _scope = (ITablePackageScope)scope;
            _model = _scope.DbContextModel;
        }

        public IEnumerable<IPackageContent> Get()
        {
            var schema = storedProcedureSchemaProvider.Get();
            var storedName = storedProcedureNameProvider.Get();
            var fullTableName = _scope.EntityModel.GetTableFullName();
            var entity = _model.Entities.Single(e => e.TableFullName == fullTableName);
            var columns = entity.AllColumns.ToList();
            var keys = entity.PrimaryKeys.ToList();

             
            var spOptions = new StoredProcedureOptions(schema, storedName);
            var sp = new StoredProcedureGenerator(spOptions, keys, new SelectGenerator(fullTableName,false, columns, keys));
            var spCode = sp.Build();
            yield return new CommandTextSegment(spCode);
        }

       
    }


    internal class Column
    {
        public Column() : this(0, ColumnAlignment.None, 0) { }
        public Column(int marginLeft) : this(marginLeft, ColumnAlignment.None, 0) { }
        public Column(ColumnAlignment alignment) : this(0, alignment, 0) { }
        public Column(int marginLeft, ColumnAlignment alignment) : this(marginLeft, alignment, 0) { }
        public Column(ColumnAlignment alignment, int marginRight) : this(0, alignment, marginRight) { }
        public Column(int marginLeft, ColumnAlignment alignment, int marginRight)
        {
            MarginLeft = marginLeft;
            ColumnAlignment = alignment;
            MarginRight = marginRight;
        }
        public int MarginLeft { get; }
        public int MarginRight { get; }
        public ColumnAlignment ColumnAlignment { get; }
    }


    internal class ConstanteColumn : Column
    {
        public ConstanteColumn(string value) : base()
        {
            Value = value;
        }
        public ConstanteColumn(string value, int marginLeft) : base(marginLeft)
        {
            Value = value;
        }
        public ConstanteColumn(string value, ColumnAlignment alignment) : base(alignment)
        {
            Value = value;
        }
        public ConstanteColumn(string value, int marginLeft, ColumnAlignment alignment) : base(marginLeft, alignment)
        {
            Value = value;
        }
        public ConstanteColumn(string value, ColumnAlignment alignment, int marginRight) : base(alignment, marginRight)
        {
            Value = value;
        }
        public ConstanteColumn(string value, int marginLeft, ColumnAlignment alignment, int marginRight) : base(marginLeft, alignment, marginRight)
        {
            Value = value;
        }
        public string Value { get; }
    }




    internal class SeparatorColumn : ConstanteColumn
    {
        public SeparatorColumn(string value) : base(value)
        { }
        public SeparatorColumn(string value, int marginLeft) : base(value, marginLeft)
        { }
        public SeparatorColumn(string value, ColumnAlignment alignment) : base(value, alignment)
        { }
        public SeparatorColumn(string value, int marginLeft, ColumnAlignment alignment) : base(value, marginLeft, alignment)
        { }
        public SeparatorColumn(string value, ColumnAlignment alignment, int marginRight) : base(value, alignment, marginRight)
        { }
        public SeparatorColumn(string value, int marginLeft, ColumnAlignment alignment, int marginRight) : base(value, marginLeft, alignment, marginRight)
        { }
    }


    internal enum ColumnAlignment
    {
        None = 0,
        Left = 1,
        Right = 2,
        Center = 3,
    }

    public class LineBuilderResult
    {
        public LineBuilderResult(IReadOnlyCollection<IReadOnlyCollection<string?>> data, IReadOnlyCollection<string?> lines)
        {
            Datas = data;
            Lines = lines;
        }
        public IReadOnlyCollection<IReadOnlyCollection<string?>> Datas { get; }
        public IReadOnlyCollection<string?> Lines { get; }

    }
    internal class DataFormater
    {
        private readonly int _columnsCount;
        private readonly List<List<string?>> _columns_rows = new List<List<string?>>();


        public DataFormater(params ColumnAlignment[] columns) : this(columns?.Select(e => new Column(e)).ToArray() ?? (new List<Column>()).ToArray()) { }
        public DataFormater(params Column[] columnsDefinitions)
        {
            if (columnsDefinitions == null) throw new ArgumentNullException(nameof(columnsDefinitions));
            _columnsCount = columnsDefinitions.Count();
            ColumnDefinitions = columnsDefinitions;
            for (int i = 0; i < columnsDefinitions.Count(); i++)
                _columns_rows.Add(new List<string?>());
        }

        public string SeparatorBefore { get; }
        public IReadOnlyList<Column> ColumnDefinitions { get; }
        public int RowCount { get; set; }
        public string SeparatorAfter { get; }


        public void AddRow(params string?[] rowsSegements)
        {
            if (rowsSegements == null)
                rowsSegements = new List<string?>() { null }.ToArray();


            if (rowsSegements == null) throw new ArgumentNullException(nameof(rowsSegements));
            if (rowsSegements.Count() != _columnsCount) throw new InvalidOperationException("Invalide rowsSegements count");
            for (int i = 0; i < _columnsCount; i++)
                _columns_rows[i].Add(rowsSegements[i]);
            RowCount++;
        }


        public LineBuilderResult Build()
        {
            var datas = new List<List<string?>>();
            var columnsCount = ColumnDefinitions.Count();
            for (int i = 0; i < RowCount; i++)
            {
                datas.Add(new List<string?>());
                var line = datas[i];
                for (int y = 0; y < columnsCount; y++)
                    line.Add(null);
            }


            var rowwColumn = _columns_rows.ToList();
            for (int columnIndex = 0; columnIndex < columnsCount; columnIndex++)
            {
                var columnDef = ColumnDefinitions[columnIndex];
                var rowFromColumnDef = string.Empty;
                bool fromCDef = false;
                bool separator = false;
                if (columnDef is ConstanteColumn cst)
                {
                    separator = cst is SeparatorColumn;
                    rowFromColumnDef = cst.Value;
                    fromCDef = true;
                }
                var columnData = rowwColumn[columnIndex].ToList();
                var validRows = columnData.Where(d => !string.IsNullOrEmpty(d)).ToList();
                int maxRowLenght = 0;

                if (validRows.Count > 0)
                    maxRowLenght = validRows.Max(x => x?.Length ?? 0);
                if (fromCDef && (rowFromColumnDef?.Length ?? 0) > maxRowLenght)
                    maxRowLenght = rowFromColumnDef?.Length ?? 0;
                for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
                {
                    var row = columnData[rowIndex];
                    if (fromCDef && row == null)
                        row = rowFromColumnDef;
                    if (separator)
                    {
                        if (rowIndex == 0 && columnIndex == 0)
                            row = string.Empty;
                        if (rowIndex == (RowCount - 1) && (columnIndex == columnsCount - 1))
                            row = string.Empty;
                    }

                    int padleft = 0;
                    int padRight = 0;
                    var tempData = (row ?? string.Empty);
                    switch (columnDef.ColumnAlignment)
                    {
                        case ColumnAlignment.None:
                            break;
                        case ColumnAlignment.Left:
                            padRight = maxRowLenght;
                            break;
                        case ColumnAlignment.Right:
                            padleft = maxRowLenght;
                            break;
                        case ColumnAlignment.Center:
                            {
                                padleft = (maxRowLenght + tempData.Length) / 2;
                                padRight = maxRowLenght;
                            }
                            break;
                    }
                    var data = (tempData).PadLeft(padleft).PadRight(padRight);
                    data = data.PadLeft(data.Length + columnDef.MarginLeft);
                    data = data.PadRight(data.Length + columnDef.MarginRight);

                    datas[rowIndex][columnIndex] = data;
                }
            }


            var datasResult = new List<IReadOnlyCollection<string?>>();
            var lines = new List<string>();
            foreach (var data in datas)
            {
                var line = string.Empty;
                datasResult.Add(data);
                foreach (var cell in data)
                {
                    if (cell != null)
                        line += cell;
                }
                lines.Add(line);
            }

            return new LineBuilderResult(datasResult, lines);
        }
    }
}
