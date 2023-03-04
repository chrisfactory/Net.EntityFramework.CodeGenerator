using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface ITablePackageScope : IPackageScope
    {
        IMutableEntityType EntityModel { get; }
    }
}
