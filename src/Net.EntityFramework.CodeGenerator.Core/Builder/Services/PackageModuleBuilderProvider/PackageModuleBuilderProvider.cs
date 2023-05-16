using Net.EntityFramework.CodeGenerator.Core.Tools;

namespace Net.EntityFramework.CodeGenerator.Core
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
                var tableBuilderAnnotation = entity.Table.FindAnnotation(Constants.EntityGenerateAnnotationKey);

                if (entity.EntityType.TryGetAnnotation<Func<IEntityModuleBuilder>>(Constants.EntityGenerateAnnotationKey, out var enityBuilder))
                    yield return enityBuilder() ?? throw new InvalidOperationException();
                if (entity.Table.TryGetAnnotation<Func<IEntityModuleBuilder>>(Constants.EntityGenerateAnnotationKey, out var tableBuilder))
                    yield return tableBuilder() ?? throw new InvalidOperationException();
            }


            if (_ModelExtractor.Model.TryGetAnnotation<Func<IModelModuleBuilder>>(Constants.ModelGenerateAnnotationKey, out var modelBuilder))
                yield return modelBuilder() ?? throw new InvalidOperationException();

        }
    }
}
