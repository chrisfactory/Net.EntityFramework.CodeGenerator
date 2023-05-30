using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexSource : ICreateIndexSource
    { 
        public CreateIndexSource(IMutableEntityType entity)
        {
            Schema = entity.GetSchema();
            Name = entity.GetTableName();
        }
        public string? Name { get; }
        public string? Schema { get; } 
    }
}
