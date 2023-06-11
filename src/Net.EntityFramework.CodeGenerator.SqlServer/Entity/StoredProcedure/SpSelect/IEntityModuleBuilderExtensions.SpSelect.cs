using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelect<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
           where TEntity : class
        {
            return module.SpSelect(builder =>
            {
                builder.SetSpSchema(schema);
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpSelect<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.SpSelect(builder =>
            {
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpSelect<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.UsePackageBuilder<ISpSelectPackageBuilder<TEntity>, SpSelectPackageBuilder<TEntity>>(ConfigureSelect(configure));
        }

        private static Action<ISpSelectPackageBuilder<TEntity>> ConfigureSelect<TEntity>(Action<ISpSelectPackageBuilder<TEntity>>? configure)
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
