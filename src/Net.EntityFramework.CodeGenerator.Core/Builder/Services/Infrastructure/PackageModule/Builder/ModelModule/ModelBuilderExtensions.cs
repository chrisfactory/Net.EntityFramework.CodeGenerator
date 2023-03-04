using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ModelBuilderExtensions
    {
        public static ModelBuilder Generate(this ModelBuilder entityBuilder, Action<IModelModuleBuilder> builder)
        {
            IModelModuleBuilder b = new ModelModuleBuilder(entityBuilder.Model);
            builder?.Invoke(b);
            entityBuilder.HasAnnotation(Constants.ModelGenerateAnnotationKey, b);
            return entityBuilder;
        }
    }
}
