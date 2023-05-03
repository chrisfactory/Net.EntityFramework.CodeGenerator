using Net.EntityFramework.CodeGenerator.Core;
using System.Text;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal interface ISqlContent
    {
        void Build(StringBuilder builder);
    }


    internal class StoredProcedureOptions
    {
        public StoredProcedureOptions(string? schemaName, string name)
        {
            Name = name;
            SchemaName = schemaName;
        }
        public string Name { get; set; }
        public string? SchemaName { get; set; }
        public bool SetNoCount { get; set; } = true;
    }


    internal class StoredProcedureGenerator
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private readonly string _schema;
        private readonly string _name;
        private readonly bool _setNoCount;
        private readonly ISqlContent _content;
        private readonly IEnumerable<IEntityColumn> _parameters;
        public StoredProcedureGenerator(StoredProcedureOptions options, IEnumerable<IEntityColumn> parameters, ISqlContent content)
        {
            _parameters = parameters;
            _schema = GetSchema(options.SchemaName);
            _name = options.Name;
            _setNoCount = options.SetNoCount;
            _content = content;
        }




        public string Build()
        {
            _builder.AppendLine($"CREATE PROCEDURE {_schema}[{_name}]");
            _builder.AppendLine("(");
            BuildParams(_builder, _parameters);
            _builder.AppendLine(")");
            _builder.AppendLine("AS");
            _builder.AppendLine("BEGIN");
            if (_setNoCount)
                _builder.AppendLine("   SET NOCOUNT ON;");
            _builder.AppendLine();
            _content.Build(_builder);
            _builder.AppendLine();
            _builder.AppendLine("END");
            return _builder.ToString();
        }

        private static void BuildParams(StringBuilder builder, IEnumerable<IEntityColumn> keys)
        {
            var name = new Column(3, ColumnAlignment.Left);
            var sqlType = new Column(2);
            var sep = new SeparatorColumn(",");
            var requ = new DataFormater(name, sqlType, sep);

            foreach (var column in keys)
            {
                requ.AddRow($"@{column.ColumnName}", column.SqlType, null);
            }

            var result = requ.Build();

            foreach (var item in result.Lines)
            {
                builder.AppendLine(item);
            }
        }


        private string GetSchema(string? schemaName)
        {
            return !string.IsNullOrEmpty(schemaName) ? $"[{schemaName}]." : string.Empty;
        }

    }
}
