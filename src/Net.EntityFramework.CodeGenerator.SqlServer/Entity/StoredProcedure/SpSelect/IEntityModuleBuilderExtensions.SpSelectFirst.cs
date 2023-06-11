using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectFirst<TEntity>(this IEntityModuleBuilder<TEntity> module, string? schema, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(schema, name, ConfigureSelectFirst(configure));

        public static IPackageToken SpSelectFirst<TEntity>(this IEntityModuleBuilder<TEntity> module, string name, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(name, ConfigureSelectFirst(configure));

        public static IPackageToken SpSelectFirst<TEntity>(this IEntityModuleBuilder<TEntity> module, Action<ISpSelectPackageBuilder<TEntity>>? configure = null)
            where TEntity : class
            => module.SpSelect(ConfigureSelectFirst(configure));


        private static Action<ISpSelectPackageBuilder<TEntity>> ConfigureSelectFirst<TEntity>(Action<ISpSelectPackageBuilder<TEntity>>? configure) 
            where TEntity : class
        
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.First());
                configure?.Invoke(builder);
            };
        }

    }
}
