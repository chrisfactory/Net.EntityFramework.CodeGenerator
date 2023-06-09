using EntityFramework.CodeGenerator.CodeGen.Tools;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.Core
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
            CallPropertyName = ToCallPropertyName(PropertyName);
            PropertyType = item.ProviderClrType.ToCSharpString();

            Column = item;
        }
        public bool IsPrimaryKey { get; }

        public string ColumnName { get; }
        public string SqlType { get; }

        public PropertyInfo? PropertyInfo { get; }
        public string? PropertyName { get; }
        public string? CallPropertyName { get; }
        public string PropertyType { get; }

        public IColumn Column { get; }


        private string? ToCallPropertyName(string? propertyName)
        {
            if(string.IsNullOrWhiteSpace(propertyName)) return null;

           return $"{char.ToLower(propertyName[0])}{propertyName.Substring(1)}"; 
        }


    }
}
