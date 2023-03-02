using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleBuilderProvider : IPackageModuleBuilderProvider
    {
        private readonly IDbContextModelExtractor _ModelExtractor;
        public PackageModuleBuilderProvider(IDbContextModelExtractor model)
        {
            _ModelExtractor = model;
        }

        public IEnumerable<IPackageModuleBuilder> Get()
        {
            foreach (var entity in _ModelExtractor.Entities)
            {
                var entityBuilderAnnotation = entity.EntityType.FindAnnotation(EntityTypeBuilderExtensions.EntityGenerateAnnotationKey);
                var tableBuilderAnnotation = entity.Table.FindAnnotation(EntityTypeBuilderExtensions.EntityGenerateAnnotationKey);

                if (entityBuilderAnnotation != null && entityBuilderAnnotation.Value != null)
                    yield return ((IPackageModuleBuilder)entityBuilderAnnotation.Value);
                else if (tableBuilderAnnotation != null && tableBuilderAnnotation.Value != null)
                    yield return ((IPackageModuleBuilder)tableBuilderAnnotation.Value);
            }
        }
    }
}
