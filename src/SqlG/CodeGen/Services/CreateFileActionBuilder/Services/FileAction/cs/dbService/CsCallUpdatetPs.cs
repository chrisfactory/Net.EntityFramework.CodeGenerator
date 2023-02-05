using System.Text;

namespace SqlG
{
    internal class CsCallUpdatetPs : IContentFileSegment
    {
        private readonly string _name;
        private readonly IEntityTypeTable _entity;
        public CsCallUpdatetPs(string name, IEntityTypeTable entity)
        {
            _name = name;
            _entity = entity;
        }
        public void Build(StringBuilder builder)
        {
            var clrEntity = _entity.EntityType;
            var tableEntity = _entity.Table;

            builder.AppendLine($"    public async Task<{clrEntity.ClrType.Name}?> {_name}Async({clrEntity.ClrType.Name} data)");
            builder.AppendLine("    {");
            builder.AppendLine("        var parameters = data.Map();");
            builder.AppendLine($"        using (var reader = await base.ExecuteReaderAsync(\"{_name}\", parameters))");
            builder.AppendLine("        { ");
            builder.AppendLine("            if (await reader.ReadAsync())");
            builder.AppendLine($"                return new {clrEntity.ClrType.Name}().Map(reader);");
            builder.AppendLine("            return null;");
            builder.AppendLine("        } ");
            builder.AppendLine("    }");
        } 

    }
}
