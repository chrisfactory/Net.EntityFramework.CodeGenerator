using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectSingle(this IEntityModuleBuilder module, string? schema, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(schema, name, ConfiguerSelectSingle(configure));

        public static IPackageToken SpSelectSingle(this IEntityModuleBuilder module, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(name, ConfiguerSelectSingle(configure));

        public static IPackageToken SpSelectSingle(this IEntityModuleBuilder module, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(ConfiguerSelectSingle(configure));


        private static Action<ISpSelectPackageBuilder> ConfiguerSelectSingle(Action<ISpSelectPackageBuilder>? configure)
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(SelectResultSet.SelectSingle());
                configure?.Invoke(builder);
            };
        }

    }
}
