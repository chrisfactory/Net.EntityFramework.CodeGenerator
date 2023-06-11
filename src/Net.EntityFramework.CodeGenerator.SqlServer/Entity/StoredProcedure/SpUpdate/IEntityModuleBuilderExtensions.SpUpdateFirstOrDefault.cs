using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpUpdateFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpUpdate(schema, name, ConfigureUpdateFirstOrDefault(configure));

        public static IPackageToken SpUpdateFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpUpdate(name, ConfigureUpdateFirstOrDefault(configure));

        public static IPackageToken SpUpdateFirstOrDefault<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpUpdatePackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpUpdate(ConfigureUpdateFirstOrDefault(configure));


        private static Action<ISpUpdatePackageBuilder<TEntity>> ConfigureUpdateFirstOrDefault<TEntity>(Action<ISpUpdatePackageBuilder<TEntity>>? configure)
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
