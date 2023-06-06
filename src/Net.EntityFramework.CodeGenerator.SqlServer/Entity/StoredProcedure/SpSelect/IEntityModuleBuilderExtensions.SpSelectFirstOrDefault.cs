using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelectFirstOrDefault(this IEntityModuleBuilder module, string? schema, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(schema, name, ConfiguerSelectFirstOrDefault(configure));

        public static IPackageToken SpSelectFirstOrDefault(this IEntityModuleBuilder module, string name, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(name, ConfiguerSelectFirstOrDefault(configure));

        public static IPackageToken SpSelectFirstOrDefault(this IEntityModuleBuilder module, Action<ISpSelectPackageBuilder>? configure = null)
            => module.SpSelect(ConfiguerSelectFirstOrDefault(configure));


        private static Action<ISpSelectPackageBuilder> ConfiguerSelectFirstOrDefault(Action<ISpSelectPackageBuilder>? configure)
        {
            return (builder) =>
            {
                builder.Services.TryAddSingleton(SelectResultSet.SelectFirstOrDefault());
                configure?.Invoke(builder); 
            };
        }

    }
}
