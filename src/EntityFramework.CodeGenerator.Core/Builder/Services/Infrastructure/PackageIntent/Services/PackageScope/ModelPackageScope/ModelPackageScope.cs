using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    internal class ModelPackageScope : IModelPackageScope
    {
        public ModelPackageScope(IMutableModel model, IDbContextModelExtractor dbContext)
        {
            DbContextModel = dbContext;
            Model = model;
        }
        public IDbContextModelExtractor DbContextModel { get; }
        public IMutableModel Model { get; }

        public string GetDisplayName()
        {
            return $"{Model}";
        }
    }
}
