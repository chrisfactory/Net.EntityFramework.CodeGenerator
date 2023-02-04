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
        private readonly IModel _model;
        private readonly IEntityType _entity;
        public CsDbAccess(CreateTableOperation operation, string name, IModel model, IEntityType entity)
        {
            _operation = operation;
            _name = name;
            _model = model;
            _entity = entity;
        }

        public void Build(StringBuilder builder)
        {
            string typeName = _entity.ClrType.Name;
      

            if (_entity.ClrType == typeof(System.Collections.Generic.Dictionary<string, object>))
                return;

             
            builder.AppendLine($"using {typeof(DBAccessBase).Namespace};");
            builder.AppendLine();
            builder.AppendLine($"internal partial class {_name} : {nameof(DBAccessBase)}//, I{_name}");
            builder.AppendLine("{");
            builder.AppendLine($"    public {_name}(string cnx) : base(cnx)");
            builder.AppendLine("    { }");
            builder.AppendLine();




            builder.AppendLine();
            builder.AppendLine("}");



        }

        public IEnumerator<IContentFileSegment> GetEnumerator()
        {
            return Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }
    }
}
