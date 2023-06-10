using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpInsert<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpInsertPackageBuilder<TEntity>>? configure = null)
           where TEntity : class
        {
            return module.SpInsert(builder =>
            {
                builder.SetSpSchema(schema);
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpInsert<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpInsertPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.SpInsert(builder =>
            {
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpInsert<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpInsertPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.UsePackageBuilder<ISpInsertPackageBuilder<TEntity>, SpInsertPackageBuilder<TEntity>>(configure);
        }
         
    }
}
