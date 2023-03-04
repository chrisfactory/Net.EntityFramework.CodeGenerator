using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class EntityTypeTable : IEntityTypeTable
    {
        public EntityTypeTable(
            IModel model,
            IEntityType entityType,
            ITable table)
        {
            Model = model;
            EntityType = entityType;
            Table = table;
            TableFullName = GetTableFullName(table.Schema, table.Name);
            LoadColumns();
        }
        public IModel Model { get; }
        public IEntityType EntityType { get; }
        public ITable Table { get; }
        public string TableFullName { get; }


        public override string ToString() => $"{EntityType.Name} => {Table.Name}";

        public IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; private set; } = new List<IEntityColumn>();
        public IReadOnlyCollection<IEntityColumn> UpdatableColumns { get; private set; } = new List<IEntityColumn>();
        public IReadOnlyCollection<IEntityColumn> InsertColumns { get; private set; } = new List<IEntityColumn>();
        public IReadOnlyCollection<IEntityColumn> AllColumns { get; private set; } = new List<IEntityColumn>();



        private void LoadColumns()
        {
            var primaryKeys = new List<IEntityColumn>();
            var updatableColumns = new List<IEntityColumn>();
            var insertColumns = new List<IEntityColumn>();
            var allColumns = new List<IEntityColumn>();
            var pks = new HashSet<IColumn>();
            if (Table.PrimaryKey != null)
            {
                foreach (var pk in Table.PrimaryKey.Columns.ToList())
                    pks.Add(pk);

            }


            var dictionaryProps = new Dictionary<string, PropertyInfo>();
            var runtimeProps = EntityType.GetRuntimeProperties().Values.ToList();
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
            foreach (var item in Table.Columns)
            {
                PropertyInfo? pi = null;
                dictionaryProps.TryGetValue(item.Name, out pi);
                var column = new EntityColumn(item, pks.Contains(item), pi);
                if (column.IsPrimaryKey)
                    primaryKeys.Add(column);
                else if (column.PropertyInfo != null)
                    updatableColumns.Add(column);
                allColumns.Add(column);
            }

            PrimaryKeys = primaryKeys;
            UpdatableColumns = updatableColumns;
            InsertColumns = insertColumns;
            AllColumns = allColumns;
        }

        public static string GetTableFullName(string? schema, string tableName)
        {
            var s = string.IsNullOrEmpty(schema) ? "" : $"{schema}.";
            return $"{s}{tableName}";
        }
    }
}
