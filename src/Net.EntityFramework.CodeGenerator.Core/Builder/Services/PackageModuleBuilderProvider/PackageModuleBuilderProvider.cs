using Net.EntityFramework.CodeGenerator.Core.Tools;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class PackageModuleBuilderProvider : IPackageModuleBuilderProvider
    {
        private readonly IDbContextModelContext _ModelExtractor;
        public PackageModuleBuilderProvider(IDbContextModelContext model)
        {
            _ModelExtractor = model;
        }

        public IEnumerable<IPackageModuleBuilder> Get()
        { 
            foreach (var entity in _ModelExtractor.Entities)
            {
                var tableBuilderAnnotation = entity.Table.FindAnnotation(Constants.EntityGenerateAnnotationKey);

                if (entity.EntityType.TryGetAnnotation<Func<IPackageModuleBuilder>>(Constants.EntityGenerateAnnotationKey, out var enityBuilder) && enityBuilder != null)
                    yield return enityBuilder() ?? throw new InvalidOperationException();
                if (entity.Table.TryGetAnnotation<Func<IPackageModuleBuilder>>(Constants.EntityGenerateAnnotationKey, out var tableBuilder) && tableBuilder != null)
                    yield return tableBuilder() ?? throw new InvalidOperationException();
            }


            if (_ModelExtractor.Model.TryGetAnnotation<Func<IPackageModuleBuilder>>(Constants.ModelGenerateAnnotationKey, out var modelBuilder) && modelBuilder != null)
                yield return modelBuilder() ?? throw new InvalidOperationException();

        }
    }
}
