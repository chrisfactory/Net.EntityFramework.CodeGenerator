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
    internal class CsMapExt : IContentFileRootSegment
    {
        private readonly CreateTableOperation _operation;
        private readonly string _name;
        private readonly IModel _model;
        private readonly IEntityType _entity;
        public CsMapExt(CreateTableOperation operation, string name, IModel model, IEntityType entity)
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
            var dictionaryProps = new Dictionary<string, PropertyInfo>();
            var runtimeProps = _entity.GetRuntimeProperties().Values.ToList();
            foreach (var item in runtimeProps)
            {
                if (item != null)
                {
                    var attr = item.GetCustomAttribute<ColumnAttribute>();
                    if (attr != null && !string.IsNullOrEmpty(attr.Name))
                        dictionaryProps.Add(attr.Name, item);
                    else
                        dictionaryProps.Add(item.Name, item);
                }
            }

             
            builder.AppendLine($"using {typeof(IDataRecord).Namespace};");
            builder.AppendLine($"using {typeof(Tools).Namespace};");
            builder.AppendLine($"using {_entity.ClrType.Namespace};");
            builder.AppendLine();
            builder.AppendLine($"public static partial class {_name}");
            builder.AppendLine("{");




            builder.AppendLine($"    public static {typeName} Map(this {typeName} data, {nameof(IDataRecord)} dataRecord)");
            builder.AppendLine("    {");
            foreach (var item in _operation.Columns)
            {
                if (dictionaryProps.ContainsKey(item.Name))
                {
                    var clrProp = dictionaryProps[item.Name];
                    var comment = item.Name != clrProp.Name ? $"// => {item.Name}" : string.Empty;
                    builder.AppendLine($"        data.{clrProp.Name} = dataRecord.Get<{clrProp.PropertyType.ToCSharpString()}>(nameof({typeName}.{clrProp.Name}));{comment}");
                }
            }
            builder.AppendLine($"        return data;");
            builder.AppendLine("    }");


            builder.AppendLine("");



            builder.AppendLine($"    public static IReadOnlyDictionary<string, object?> Map(this {typeName} data)");
            builder.AppendLine("    {");
            builder.AppendLine("        var result = new Dictionary<string, object?>");
            builder.AppendLine("        {");
            var i = _operation.Columns.Count;
            foreach (var item in _operation.Columns)
            {
                i--;
                if (dictionaryProps.ContainsKey(item.Name))
                {
                    var clrProp = dictionaryProps[item.Name];
                    string end = (i > 0) ? "," : "";
                    builder.AppendLine($"            {{ \"@{item.Name}\", data.{clrProp.Name} }}{end}");
                }
            }
            builder.AppendLine("        };");
            builder.AppendLine($"        return result;");
            builder.AppendLine("    }");
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
