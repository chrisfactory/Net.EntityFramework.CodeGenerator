using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    internal class TablePackageScope : ITablePackageScope
    {
        public TablePackageScope(IMutableEntityType metaData, IDbContextModelExtractor dbContext)
        {
            DbContextModel = dbContext;
            EntityModel = metaData;
        }
        public IDbContextModelExtractor DbContextModel { get; }
        public IMutableEntityType EntityModel { get; }


        public string GetDisplayName()
        {
            return $"{EntityModel.DisplayName()} => {EntityModel.GetTableName()}";
        }
    }
}
