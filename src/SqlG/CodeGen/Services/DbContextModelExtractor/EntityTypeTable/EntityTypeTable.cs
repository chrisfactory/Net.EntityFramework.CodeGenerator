using Microsoft.EntityFrameworkCore.Metadata;

namespace SqlG
{
    internal class EntityTypeTable : IEntityTypeTable
    {
        public EntityTypeTable(IModel model, IEntityType entityType, ITable table)
        {
            Model = model;
            EntityType = entityType;
            Table = table;
            
        }
        public IModel Model { get; }
        public IEntityType EntityType { get; }
        public ITable Table { get; }


        public override string ToString() => $"{EntityType.Name} => {Table.Name}";

    }
}
