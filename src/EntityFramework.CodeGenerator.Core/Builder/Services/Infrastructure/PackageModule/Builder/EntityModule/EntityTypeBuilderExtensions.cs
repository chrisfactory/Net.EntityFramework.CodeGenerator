using EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class EntityTypeBuilderExtensions
    {
        internal const string EntityGenerateAnnotationKey = nameof(EntityGenerateAnnotationKey);
        public static EntityTypeBuilder Generate(this EntityTypeBuilder entityBuilder, Action<IEntityModuleBuilder> builder)
        {
            IEntityModuleBuilder b = new EntityModuleBuilder(entityBuilder.Metadata);
            builder?.Invoke(b);
            entityBuilder.HasAnnotation(EntityGenerateAnnotationKey, b);
            return entityBuilder;
        }
    }
}
