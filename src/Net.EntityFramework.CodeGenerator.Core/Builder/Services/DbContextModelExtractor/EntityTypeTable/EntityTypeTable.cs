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
            TableFullName = table.GetTableFullName();
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
            var allColumns = new List<IEntityColumn>();
            var pks = new HashSet<IColumn>();

            if (Table.PrimaryKey != null)
            {
                foreach (var pk in Table.PrimaryKey.Columns.ToList())
                    pks.Add(pk);
            }

        

            foreach (var item in Table.Columns)
            {
                var isPk = pks.Contains(item);

                var column = new EntityColumn(item, isPk);
             
                if (column.IsPrimaryKey)
                    primaryKeys.Add(column);
                else //if (column.PropertyInfo != null)
                    updatableColumns.Add(column);
                allColumns.Add(column);

            }

            PrimaryKeys = primaryKeys;
            UpdatableColumns = updatableColumns;
            InsertColumns = updatableColumns;
            AllColumns = allColumns;
        }

       
    }
}
