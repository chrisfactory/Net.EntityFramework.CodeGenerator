using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectFirst(this IEntityModuleBuilder module, string? schema, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(schema, name, ConfiguerSelectFirst(configure));

        public static IPackageToken SpSelectFirst(this IEntityModuleBuilder module, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(name, ConfiguerSelectFirst(configure));

        public static IPackageToken SpSelectFirst(this IEntityModuleBuilder module, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(ConfiguerSelectFirst(configure));


        private static Action<ISpSelectPackageBuilder> ConfiguerSelectFirst(Action<ISpSelectPackageBuilder>? configure)
        {
            return (builder) =>
            {
                configure?.Invoke(builder);
            };
        }

    }
}
