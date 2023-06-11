using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpUpdate<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
           where TEntity : class
        {
            return module.SpUpdate(builder =>
            {
                builder.SetSpSchema(schema);
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpUpdate<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.SpUpdate(builder =>
            {
                builder.SetSpName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpUpdate<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
        {
            return module.UsePackageBuilder<ISpUpdatePackageBuilder<TEntity>, SpUpdatePackageBuilder<TEntity>>(ConfigureUpdate(configure));
        }

        private static Action<ISpUpdatePackageBuilder<TEntity>> ConfigureUpdate<TEntity>(Action<ISpUpdatePackageBuilder<TEntity>>? configure)
          where TEntity : class
        {
            return (builder) =>
            {
                configure?.Invoke(builder);
                builder.Services.TryAddSingleton(ResultSetProvider.None());
            };
        }

    }
}
