using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    internal class TablePackageScope : ITablePackageScope
    {
        public TablePackageScope(IMutableEntityType metaData)
        {
            MetaData = metaData;
        }

        public IMutableEntityType MetaData { get; }

        public string GetDisplayName()
        {
            return $"{MetaData.DisplayName()}";
        }
    }
}
