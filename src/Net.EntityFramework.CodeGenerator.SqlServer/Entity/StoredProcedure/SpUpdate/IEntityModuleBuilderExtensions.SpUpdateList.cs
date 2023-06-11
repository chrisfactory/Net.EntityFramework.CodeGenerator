using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpUpdateList<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpUpdate(schema, name, ConfigureUpdateList(configure));

        public static IPackageToken SpUpdateList<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpUpdate(name, ConfigureUpdateList(configure));

        public static IPackageToken SpUpdateList<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpUpdate(ConfigureUpdateList(configure));


        private static Action<ISpUpdatePackageBuilder<TEntity>> ConfigureUpdateList<TEntity>(Action<ISpUpdatePackageBuilder<TEntity>>? configure) 
            where TEntity : class
        
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.List());
                configure?.Invoke(builder);
            };
        }

    }
}
