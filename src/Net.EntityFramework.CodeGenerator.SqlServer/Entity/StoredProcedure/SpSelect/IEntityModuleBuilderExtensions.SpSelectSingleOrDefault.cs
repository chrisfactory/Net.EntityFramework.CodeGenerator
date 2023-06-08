using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectSingleOrDefault(this IEntityModuleBuilder module, string? schema, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(schema, name, ConfiguerSelectSingleOrDefault(configure));

        public static IPackageToken SpSelectSingleOrDefault(this IEntityModuleBuilder module, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(name, ConfiguerSelectSingleOrDefault(configure));

        public static IPackageToken SpSelectSingleOrDefault(this IEntityModuleBuilder module, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(ConfiguerSelectSingleOrDefault(configure));


        private static Action<ISpSelectPackageBuilder> ConfiguerSelectSingleOrDefault(Action<ISpSelectPackageBuilder>? configure)
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(ResultSetProvider.SelectSingleOrDefault());
                configure?.Invoke(builder);
            };
        }

    }
}
