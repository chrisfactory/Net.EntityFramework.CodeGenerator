using EntityFramework.CodeGenerator.CodeGen.Tools;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class EntityColumn : IEntityColumn
    {
        public EntityColumn(IColumn item, bool isPrimaryKey, PropertyInfo? property)
        {

            IsPrimaryKey = isPrimaryKey;
            ColumnName = item.Name;
            SqlType = item.StoreType;

            PropertyInfo = property;
            PropertyName = PropertyInfo?.Name;
            PropertyType = item.ProviderClrType.ToCSharpString();

            Column = item;
        }
        public bool IsPrimaryKey { get; }

        public string ColumnName { get; }
        public string SqlType { get; }

        public PropertyInfo? PropertyInfo { get; }
        public string? PropertyName { get; }
        public string PropertyType { get; }

        public IColumn Column { get; }
    }
}
