using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    public interface ITablePackageScope : IPackageScope
    {
        IMutableEntityType MetaData { get; }
    }
}
