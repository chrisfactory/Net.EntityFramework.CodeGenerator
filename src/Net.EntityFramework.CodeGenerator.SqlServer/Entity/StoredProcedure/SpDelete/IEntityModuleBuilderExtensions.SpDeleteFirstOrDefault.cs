using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpDeleteFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(schema, name, ConfigureDeleteFirstOrDefault(configure));

        public static IPackageToken SpDeleteFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(name, ConfigureDeleteFirstOrDefault(configure));

        public static IPackageToken SpDeleteFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(ConfigureDeleteFirstOrDefault(configure));


        private static Action<ISpDeletePackageBuilder<TEntity>> ConfigureDeleteFirstOrDefault<TEntity>(Action<ISpDeletePackageBuilder<TEntity>>? configure)
            where TEntity : class
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.FirstOrDefault());
                configure?.Invoke(builder); 
            };
        }

    }
}
