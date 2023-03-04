using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageScope
    {
        string GetDisplayName();
        IDbContextModelExtractor DbContextModel { get; }
    }
}
