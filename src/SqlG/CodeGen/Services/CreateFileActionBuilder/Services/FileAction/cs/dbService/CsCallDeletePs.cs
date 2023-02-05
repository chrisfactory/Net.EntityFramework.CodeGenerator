using SqlG.CodeGen.Tools;
using System.Text;

namespace SqlG
{
    internal class CsCallDeletePs : IContentFileSegment
    {
        private readonly string _name;
        private readonly IEntityTypeTable _entity;
        public CsCallDeletePs(string name, IEntityTypeTable entity)
        {
            _name = name;
            _entity = entity;
        }
        public void Build(StringBuilder builder)
        {
            var clrEntity = _entity.EntityType;
            var tableEntity = _entity.Table;
            var pks = tableEntity.PrimaryKey?.Columns;

            string parameters = string.Empty;
            bool _a = false;
            if (pks != null)
            {
                foreach (var pk in pks)
                {
                    foreach (var pMap in pk.PropertyMappings)
                    {
                        string separator = string.Empty;
                        if (_a)
                            separator = " ,";
                        parameters += $"{separator}{pMap.TypeMapping.ClrType.ToCSharpString()} {pMap.Property.Name.ToCamelCase()}";
                        _a = true;
                    }

                }
            }


            builder.AppendLine($"    public async Task {_name}Async({parameters})");
            builder.AppendLine("    {");
            builder.AppendLine("        var parameters = new Dictionary<string, object?>");
            builder.AppendLine("        {");
            var i = pks.Count;
            foreach (var item in pks)
            {
                i--;

                string end = (i > 0) ? "," : "";
                builder.AppendLine($"            {{ \"@{item.Name}\", {item.PropertyMappings[0].Property.Name.ToCamelCase()} }}{end}");

            }
            builder.AppendLine("        };");
            builder.AppendLine($"        using (var reader = await base.ExecuteReaderAsync(\"{_name}\", parameters))");
            builder.AppendLine("        { ");
            builder.AppendLine("        } ");
            builder.AppendLine("    }");

        }


    }
}
