using DataBaseAccess;
using System.Text;
using EntityFramework.CodeGenerator.Core;
namespace EntityFramework.CodeGenerator
{
    internal class CsDbAccess : IContentFileSegment
    {
        private readonly string _name;
        private readonly IEntityTypeTable _entity;
        private readonly IContentFileSegment[] _childs;
        public CsDbAccess(string name, IEntityTypeTable entity, params IContentFileSegment[] childs)
        {
            _name = name;
            _entity = entity;
            _childs = childs;
        }

        public void Build(StringBuilder builder)
        {

            builder.AppendLine($"using {typeof(DBAccessBase).Namespace};");
            builder.AppendLine($"using {_entity.EntityType.ClrType.Namespace};");
            builder.AppendLine();
            builder.AppendLine($"public partial class {_name} : {nameof(DBAccessBase)}");
            builder.AppendLine("{");
            builder.AppendLine($"    public {_name}(string cnx) : base(cnx)");
            builder.AppendLine("    { }");
            builder.AppendLine();


            if (_childs != null)
                foreach (var item in _childs)
                { 
                    item.Build(builder);
                    builder.AppendLine();
                } 

            builder.AppendLine("}");
        }

    }
}
