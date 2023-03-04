using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageScope
    {
        string GetDisplayName();
        IDbContextModelExtractor DbContextModel { get; }
    }
}
