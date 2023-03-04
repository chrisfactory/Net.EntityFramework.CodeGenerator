using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IModelPackageScope : IPackageScope
    { 
        IMutableModel Model { get; }
    }
}
