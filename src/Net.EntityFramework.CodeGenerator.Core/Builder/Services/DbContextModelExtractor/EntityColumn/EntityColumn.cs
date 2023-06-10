using EntityFramework.CodeGenerator.CodeGen.Tools;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class EntityColumn : IEntityColumn
    {
        public EntityColumn(IColumn item, bool isPrimaryKey)
        {
            IsPrimaryKey = isPrimaryKey;
            bool isForeignKey = false;
            bool isShadowProperty = false;
            PropertyInfo = GetPropertyInfo(item, out isForeignKey, out isShadowProperty);
            IsForeignKey = isForeignKey;
            IsShadowProperty = isShadowProperty;
            //var attr = PropertyInfo?.GetCustomAttribute<ColumnAttribute>();
            //ColumnName = (attr != null && !string.IsNullOrEmpty(attr.Name)) ? attr.Name : item.Name;


            Column = item;
            ColumnName = item.Name;
            SqlType = item.StoreType;

            //PropertyInfo = property;
            PropertyType = item.ProviderClrType.ToCSharpString();
            if (item.IsNullable)
                PropertyType += "?";


        }



        public PropertyInfo? PropertyInfo { get; }

        public bool IsPrimaryKey { get; }
        public bool IsForeignKey { get; }
        public bool IsShadowProperty { get; }
        public string ColumnName { get; }
        public string SqlType { get; }


        public string? PropertyName { get; private set; }
        public string? CallPropertyName { get; private set; } 
        public string PropertyType { get; }

        public IColumn Column { get; }

        private PropertyInfo? GetPropertyInfo(IColumn item, out bool isForeignKey, out bool isShadowProperty)
        {
            PropertyInfo? pi = null;


            var mapping = item.PropertyMappings.First();
            isShadowProperty = mapping.Property.IsShadowProperty();
            isForeignKey = mapping.Property.IsForeignKey();

            if (mapping.Property.PropertyInfo != null)
            {
                pi = mapping.Property.PropertyInfo;
                PropertyName = pi.Name;
                CallPropertyName = ToCallPropertyName(PropertyName); 
            }
            else
            {
                PropertyName = item.Name;
                CallPropertyName = ToCallPropertyName(PropertyName); 
                if (isForeignKey)
                {
                    isForeignKey = true;
                    var principal = mapping.Property.FindFirstPrincipal();
                    if (principal != null)
                    {
                        pi = principal.PropertyInfo;
                        if (pi == null)
                        {
                            throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                  
                }
                else if (isShadowProperty)
                {
                   
                }
                else
                {
                    throw new NotImplementedException();

                }
            }
 
            return pi;
        }


        private string? ToCallPropertyName(string? propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return null;

            return $"{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
        }


    }
}
