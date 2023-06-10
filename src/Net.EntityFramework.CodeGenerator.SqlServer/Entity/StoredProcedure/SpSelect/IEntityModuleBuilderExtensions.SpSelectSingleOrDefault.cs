using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectSingleOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(schema, name, ConfiguerSelectSingleOrDefault(configure));

        public static IPackageToken SpSelectSingleOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(name, ConfiguerSelectSingleOrDefault(configure));

        public static IPackageToken SpSelectSingleOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(ConfiguerSelectSingleOrDefault(configure));


        private static Action<ISpSelectPackageBuilder<TEntity>> ConfiguerSelectSingleOrDefault<TEntity>(Action<ISpSelectPackageBuilder<TEntity>>? configure) 
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
