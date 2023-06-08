using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelect(this IEntityModuleBuilder module, string? schema, string name, Action<ISpSelectPackageBuilder>? configure = null)
        {
            return module.SpSelect(builder =>
            {
                builder.SetSchema(schema);
                builder.SetName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpSelect(this IEntityModuleBuilder module, string name, Action<ISpSelectPackageBuilder>? configure = null)
        {
            return module.SpSelect(builder =>
            {
                builder.SetName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpSelect(this IEntityModuleBuilder module, Action<ISpSelectPackageBuilder>? configure = null)
        {
            return module.UsePackageBuilder<ISpSelectPackageBuilder, SpSelectPackageBuilder>(ConfiguerSelect(configure));
        }

        private static Action<ISpSelectPackageBuilder> ConfiguerSelect(Action<ISpSelectPackageBuilder>? configure)
        {
            return (builder) =>
            {
                configure?.Invoke(builder); 
                builder.Services.TryAddSingleton(ResultSetProvider.Select());
            };
        }
    } 
}
