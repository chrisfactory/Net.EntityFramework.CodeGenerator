using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpDeleteEnumerable<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(schema, name, ConfigureDeleteEnumerable(configure));

        public static IPackageToken SpDeleteEnumerable<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(name, ConfigureDeleteEnumerable(configure));

        public static IPackageToken SpDeleteEnumerable<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpDeletePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpDelete(ConfigureDeleteEnumerable(configure));


        private static Action<ISpDeletePackageBuilder<TEntity>> ConfigureDeleteEnumerable<TEntity>(Action<ISpDeletePackageBuilder<TEntity>>? configure) 
            where TEntity : class
        
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.Enumerable());
                configure?.Invoke(builder);
            };
        }

    }
}
