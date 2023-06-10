using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectSingle<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(schema, name, ConfiguerSelectSingle(configure));

        public static IPackageToken SpSelectSingle<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(name, ConfiguerSelectSingle(configure));

        public static IPackageToken SpSelectSingle<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(ConfiguerSelectSingle(configure));


        private static Action<ISpSelectPackageBuilder<TEntity>> ConfiguerSelectSingle<TEntity>(Action<ISpSelectPackageBuilder<TEntity>>? configure)
            where TEntity : class
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.Single());
                configure?.Invoke(builder);
            };
        }

    }
}
