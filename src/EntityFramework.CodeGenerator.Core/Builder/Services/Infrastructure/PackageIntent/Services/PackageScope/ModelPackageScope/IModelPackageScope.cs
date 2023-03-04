using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IModelPackageScope : IPackageScope
    { 
        IMutableModel Model { get; }
    }
}
