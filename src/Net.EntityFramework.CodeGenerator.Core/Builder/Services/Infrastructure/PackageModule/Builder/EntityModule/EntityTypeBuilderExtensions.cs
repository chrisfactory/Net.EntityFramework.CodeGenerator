using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> GenerateFilesFor<TEntity>(this EntityTypeBuilder<TEntity> entityBuilder, Action<IEntityModuleBuilder<TEntity>> configure)
            where TEntity : class
        {
            entityBuilder.HasAnnotation(Constants.EntityGenerateAnnotationKey, () => (IPackageModuleBuilder)new EntityModuleBuilder<TEntity>(entityBuilder.Metadata, configure));
            return entityBuilder;
        }
    }
}
