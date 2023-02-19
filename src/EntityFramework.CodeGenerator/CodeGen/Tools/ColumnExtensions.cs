using System.Text;

namespace EntityFramework.CodeGenerator
{
    internal static class ColumnExtensions
    {
        private const int DefaultPad = 4;


        internal static void BuildCreateProcedure(this StringBuilder builder, string? schema, string name, IEnumerable<IEntityColumn> columns, int padNumber = 0)
        {
            var sc = GetShema(schema);
            var pad = PadSpace(padNumber);
            builder.AppendLine($"{pad}CREATE PROCEDURE {sc}[{name}]");
            builder.AppendLine($"{pad}(");
            builder.BuildSpParamerts(columns, true, padNumber + 1);
            builder.AppendLine($"{pad})");
            builder.AppendLine($"{pad}AS BEGIN");
        }

        internal static void BuildTalbleTemp(this StringBuilder builder, IEnumerable<IEntityColumn> columns, string name, int padNumber = 1)
        {
            var pad = PadSpace(padNumber);

            builder.AppendLine($"{pad}DECLARE {name} TABLE");
            builder.AppendLine($"{pad}(");
            builder.BuildTalbleProperties(columns, padNumber + 1);
            builder.AppendLine($"{pad})");
        }

        internal static void BuildInsertInto(this StringBuilder builder, string? schema, string name, IEnumerable<IEntityColumn> columns, int padNumber = 1)
        {
            var sc = GetShema(schema);
            var pad = PadSpace(padNumber);
            builder.AppendLine($"{pad}INSERT INTO {sc}[{name}]");
            builder.AppendLine($"{pad}(");
            builder.BuildSelecteProperties(columns, padNumber + 1);
            builder.AppendLine($"{pad})");
        }

        internal static void BuildInsertOutput(this StringBuilder builder, IEnumerable<IEntityColumn> columns, string targetName, int padNumber = 1)
        {
            var pad = PadSpace(padNumber);
            builder.AppendLine($"{pad}OUTPUT");
            builder.BuildInsertedOutpuList(columns, padNumber + 1);
            builder.AppendLine($"{PadSpace(1)}INTO {targetName}");
        }

        internal static void BuildInsertValues(this StringBuilder builder, IEnumerable<IEntityColumn> columns, int padNumber = 1)
        {
            var pad = PadSpace(padNumber);
            builder.AppendLine($"{pad}VALUES");
            builder.AppendLine($"{pad}(");
            builder.BuildSpParamerts(columns, false, padNumber + 1);
            builder.AppendLine($"{pad})");
        }

        internal static void BuildSpParamerts(this StringBuilder builder, IEnumerable<IEntityColumn> columns, bool includeType, int padNumber = 1)
        {
            builder.BuildList(columns, includeType, "@", null, padNumber);
        }

        internal static void BuildSelecteProperties(this StringBuilder builder, IEnumerable<IEntityColumn> columns, int padNumber = 1)
        {
            builder.BuildList(columns, false, "[", "]", padNumber);
        }
        internal static void BuildTalbleProperties(this StringBuilder builder, IEnumerable<IEntityColumn> columns, int padNumber = 1)
        {
            builder.BuildList(columns, true, "[", "]", padNumber);
        }

        internal static void BuildInsertedOutpuList(this StringBuilder builder, IEnumerable<IEntityColumn> columns, int padNumber = 1)
        {
            builder.BuildList(columns, false, "INSERTED.[", "]", padNumber);
        }



        private static void BuildList(this StringBuilder builder, IEnumerable<IEntityColumn> columns, bool includeType, string? befor, string? after, int padNumber = 1)
        {
            var pad = PadSpace(padNumber);

            var last = columns.Last();
            var mx = columns.Max(c => c.ColumnName.Length) + 4; ;
            foreach (var column in columns)
            {
                string end = column != last ? "," : "";
                var c = $"{befor}{column.ColumnName}{after}";
                if (includeType)
                    c = c.PadRight(mx);
                var type = includeType ? $" {column.SqlType?.ToUpper()}" : "";
                builder.AppendLine($"{pad}{c}{type}{end}");
            }
        }

        private static string PadSpace(int padNumber)
        {
            return "".PadLeft(padNumber * DefaultPad);
        }
        private static string GetShema(string? schema)
        {
            return string.IsNullOrWhiteSpace(schema) ? "" : $"[{schema}].";
        }
    }
}
