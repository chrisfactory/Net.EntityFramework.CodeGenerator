using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(schema, name, ConfiguerSelectFirstOrDefault(configure));

        public static IPackageToken SpSelectFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(name, ConfiguerSelectFirstOrDefault(configure));

        public static IPackageToken SpSelectFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(ConfiguerSelectFirstOrDefault(configure));


        private static Action<ISpSelectPackageBuilder<TEntity>> ConfiguerSelectFirstOrDefault<TEntity>(Action<ISpSelectPackageBuilder<TEntity>>? configure)
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
