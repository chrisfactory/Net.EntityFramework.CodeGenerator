using DataBaseAccess;
using System.Data;
using System.Text;
using EntityFramework.CodeGenerator.Core;
namespace EntityFramework.CodeGenerator
{
    internal class CsMapExt : IContentFileSegment
    {
        private readonly string _name;
        private readonly IEntityTypeTable _entity;
        private readonly IContentFileSegment[] _childs;
        public CsMapExt(string name, IEntityTypeTable entity, params IContentFileSegment[] childs)
        {
            _name = name;
            _entity = entity;
            _childs = childs;
        }

        public void Build(StringBuilder builder)
        {
            var clrType = _entity.EntityType.ClrType;

            if (clrType == typeof(System.Collections.Generic.Dictionary<string, object>))
                return;


            builder.AppendLine($"using {typeof(IDataRecord).Namespace};");
            builder.AppendLine($"using {typeof(Tools).Namespace};");
            builder.AppendLine($"using {clrType.Namespace};");
            builder.AppendLine();
            builder.AppendLine($"public static partial class {_name}");
            builder.AppendLine("{");

            if (_childs != null)
                foreach (var child in _childs)
                {
                    child.Build(builder);
                    builder.AppendLine("");
                }

            builder.AppendLine("}");

        }
    }
}
