using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder Generate(this EntityTypeBuilder entityBuilder, Action<IEntityModuleBuilder> configure)
        { 
            entityBuilder.HasAnnotation(Constants.EntityGenerateAnnotationKey, ()=> (IEntityModuleBuilder)new EntityModuleBuilder(entityBuilder.Metadata, configure));
            return entityBuilder;
        }
    }
}
