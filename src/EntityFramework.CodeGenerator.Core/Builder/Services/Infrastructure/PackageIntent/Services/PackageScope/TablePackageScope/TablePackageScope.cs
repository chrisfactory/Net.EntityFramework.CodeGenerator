using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    internal class TablePackageScope : ITablePackageScope
    {
        public TablePackageScope(IMutableEntityType metaData, IDbContextModelExtractor model)
        {
            MetaData = metaData;
        }

        public IMutableEntityType MetaData { get; }

        public string GetDisplayName()
        {
            return $"{MetaData.DisplayName()} => {MetaData.GetTableName()}";
        }
    }
}
