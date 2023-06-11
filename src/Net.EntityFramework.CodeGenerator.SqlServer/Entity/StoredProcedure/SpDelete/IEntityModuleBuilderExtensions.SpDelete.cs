using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpDelete<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
           where TEntity : class
        {
            return module.SpDelete(builder =>
            {
                builder.SetSpSchema(schema);
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpDelete<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.SpDelete(builder =>
            {
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpDelete<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.UsePackageBuilder<ISpDeletePackageBuilder<TEntity>, SpDeletePackageBuilder<TEntity>>(ConfigureDelete(configure));
        }

        private static Action<ISpDeletePackageBuilder<TEntity>> ConfigureDelete<TEntity>(Action<ISpDeletePackageBuilder<TEntity>>? configure)
            where TEntity : class
        {
            return (builder) =>
            {
                configure?.Invoke(builder);
                builder.Services.TryAddSingleton(ResultSetProvider.Default());
            };
        }
    }
}
