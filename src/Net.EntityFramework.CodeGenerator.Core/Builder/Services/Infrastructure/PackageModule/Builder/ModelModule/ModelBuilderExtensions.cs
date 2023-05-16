using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ModelBuilderExtensions
    {
        public static ModelBuilder Generate(this ModelBuilder entityBuilder, Action<IModelModuleBuilder> configure)
        { 
            entityBuilder.HasAnnotation(Constants.ModelGenerateAnnotationKey, () => (IModelModuleBuilder)new ModelModuleBuilder(entityBuilder.Model, configure));
            return entityBuilder;
        }
    }
}
