using EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ModelBuilderExtensions
    {
        internal const string ModelGenerateAnnotationKey = nameof(ModelGenerateAnnotationKey);
        public static ModelBuilder Generate(this ModelBuilder entityBuilder, Action<IModelModuleBuilder> builder)
        {
            IModelModuleBuilder b = new ModelModuleBuilder(entityBuilder.Model);
            builder?.Invoke(b);
            entityBuilder.HasAnnotation(ModelGenerateAnnotationKey, b);
            return entityBuilder;
        }
    }
}
