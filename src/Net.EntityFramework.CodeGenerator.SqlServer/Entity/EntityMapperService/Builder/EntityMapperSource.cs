using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EntityMapperSource : IEntityMapperCodeGeneratorSource
    {
        public EntityMapperSource(IMutableEntityType entity)
        {
            Entity = entity; 
            Schema = entity.GetSchema();
            Name = entity.GetTableName();
        }
        
        public string Schema { get; } 
        public string Name { get; }

        public IMutableEntityType Entity { get; }
    }
}
