using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Linq;

namespace SqlG
{
    internal class EntitySchemaFactory : IEntitySchemaFactory
    {
        private readonly IEntityModel _model;
        public EntitySchemaFactory(IEntityModel model)
        {
            _model = model;
            GetProperties();
        }
        public IEntitySchema Create()
        {
            return null;
        }


        public void GetProperties()
        {
            var type = _model.ModelType;

            foreach (var pInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (CanInclude(pInfo))
                {
                    var propertyBuilder = GetProvider(pInfo);

                    if (propertyBuilder != null)
                    {

                        var clmn = propertyBuilder.Build();
                    }
                }

                //bool isKey = false;
                //foreach (var attr in item.GetCustomAttributes())
                //{
                //    //ConcurrencyCheck
                //    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
                //    //StringLength
                //    //MaxLength
                //    //InverseProperty
                //    //Index
                //    //ForeignKey
                //    //NotMapped
                //    //Timestamp
                //    //if (attr is KeyAttribute key)
                //    //{
                //    //    isKey = true;
                //    //}
                //    //else if (attr is [Required] attribute) { }
                //    //Column
                //}

            }
        }

        private bool CanInclude(PropertyInfo? pInfo)
        {
            if (pInfo == null) return false;
            if (!pInfo.CanRead) return false;
            if (!pInfo.CanWrite) return false;
            if (pInfo.PropertyType == null) return false;

            var notMapped = pInfo.GetCustomAttribute<NotMappedAttribute>();
            if (notMapped != null) return false;

            return true;
        }


        public static IEntityColumnBuilder GetProvider(PropertyInfo pInfo)
        {
            if (pInfo.PropertyType == typeof(int))
            {
                return new IntColomnBuilder(pInfo);
            }
            if (pInfo.PropertyType == typeof(int?))
            {
                return null;
            }
            return null;
        }
    }
    public interface IEntityColumnBuilder
    {
        public IEntityColumn Build();
    }

    internal class EntityColumn : IEntityColumn
    {
        public EntityColumn(
            Type type,
            int? order,
            string modelName,
            string dbName, bool riquierd)
        {
            Type = type;
            Order = order;
            ModelName = modelName;
            DbName = dbName;
            Riquierd = riquierd;
        }
        public Type Type { get; }
        public int? Order { get; }
        public string ModelName { get; }
        public string DbName { get; }
        public bool Riquierd { get; }
    }

    public abstract class ColomnBuilderBase : IEntityColumnBuilder
    {
        private readonly PropertyInfo _pInfo;
        protected ColomnBuilderBase(PropertyInfo propertyInfo)
        {
            _pInfo = propertyInfo;
        }
        public abstract IEntityColumn Build();

        protected ColumnConfig GetConfiguration()
        {
            bool isKey = false;
            bool isRequired = false;
            int? maxLength = null;
            int? stringMaxLength = null;
            int? stringMinLength = null;
            string modelName = _pInfo.Name;
            string dbName = _pInfo.Name;
            int? order = null;
            string? dbTypeName = null;
            foreach (var attr in _pInfo.GetCustomAttributes())
            {

                if (attr is KeyAttribute key)
                    isKey = true;
                else if (attr is RequiredAttribute required)
                    isRequired = true;
                else if (attr is ColumnAttribute column)
                {
                    if (!string.IsNullOrEmpty(column.Name))
                        dbName = column.Name;
                    if (column.Order >= 0)
                        order = column.Order;
                    dbTypeName = column.TypeName;
                }

                else if (attr is MaxLengthAttribute ml)
                    maxLength = ml.Length;
                else if (attr is StringLengthAttribute sl)
                {
                    stringMaxLength = sl.MaximumLength;
                    stringMinLength = sl.MinimumLength;
                }

                //ConcurrencyCheck
                //[DatabaseGenerated(DatabaseGeneratedOption.None)]

                //InverseProperty
                //Index
                //ForeignKey 
                //Timestamp

                //else if (attr is [Required] attribute) { }
                //Column
            }
            return new ColumnConfig
            {
                IsRequired = isRequired,
                Order = order,
                ModelName = modelName,
                DbName = dbName,
                IsKey = isKey,
                MaxLength = maxLength,
                StringMaximumLength = stringMaxLength,
                StringMinimumLength = stringMinLength,

            };
        }
        protected struct ColumnConfig
        {
            public bool IsRequired { get; set; }
            public int? Order { get; set; }
            public string ModelName { get; set; }
            public string DbName { get; set; }
            public bool IsKey { get; set; }
            public int? MaxLength { get; set; }
            public int? StringMaximumLength { get; set; }
            public int? StringMinimumLength { get; set; }
        }


    }
    public class IntColomnBuilder : ColomnBuilderBase
    {
        public IntColomnBuilder(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }
        public override IEntityColumn Build()
        {
            var config = base.GetConfiguration();
            return new IntColomn(config.Order, config.ModelName, config.DbName);
        }
        private class IntColomn : IEntityColumn
        {
            public IntColomn(int? order, string modelName, string dbName)
            {
                Type = typeof(int);
                Riquierd = true;
                Order = order;
                ModelName = modelName;
                DbName = dbName;
            }
            public Type Type { get; }
            public bool Riquierd { get; }
            public int? Order { get; }
            public string ModelName { get; }
            public string DbName { get; }
        }
    }
}
