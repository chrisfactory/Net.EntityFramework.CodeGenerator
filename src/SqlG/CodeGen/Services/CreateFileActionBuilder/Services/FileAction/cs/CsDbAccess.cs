using DataBaseAccess;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SqlG.CodeGen.Tools;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

namespace SqlG
{
    internal class CsDbAccess : IContentFileRootSegment
    {
        private readonly CreateTableOperation _operation;
        private readonly string _name; 
        private readonly IEntityTypeTable _entity;
        private readonly IContentFileSegment[] _childs;
        public CsDbAccess(CreateTableOperation operation, string name, IEntityTypeTable entity, params IContentFileSegment[] childs)
        {
            _operation = operation;
            _name = name; 
            _entity = entity;
            _childs = childs;
        }

        public void Build(StringBuilder builder)
        {
            string typeName = _entity.EntityType.ClrType.Name;


            if (_entity.EntityType.ClrType == typeof(System.Collections.Generic.Dictionary<string, object>))
                return;


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
                    item.Build(builder);

            builder.AppendLine();
            builder.AppendLine("}");



        }

        public IEnumerator<IContentFileSegment> GetEnumerator()
        {
            return _childs?.Cast<IContentFileSegment>().GetEnumerator() ?? Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _childs?.GetEnumerator() ?? Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }
    }
}
