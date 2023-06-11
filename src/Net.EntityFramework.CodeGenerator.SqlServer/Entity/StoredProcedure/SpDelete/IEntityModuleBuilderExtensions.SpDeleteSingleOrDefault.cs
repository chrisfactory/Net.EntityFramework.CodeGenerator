using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpDeleteSingleOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(schema, name, ConfigureDeleteSingleOrDefault(configure));

        public static IPackageToken SpDeleteSingleOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(name, ConfigureDeleteSingleOrDefault(configure));

        public static IPackageToken SpDeleteSingleOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(ConfigureDeleteSingleOrDefault(configure));


        private static Action<ISpDeletePackageBuilder<TEntity>> ConfigureDeleteSingleOrDefault<TEntity>(Action<ISpDeletePackageBuilder<TEntity>>? configure) 
            where TEntity : class
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.SingleOrDefault());
                configure?.Invoke(builder);
            };
        }

    }
}
