using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken SpSelect(this IEntityModuleBuilder module, string? schema, string name, Action<ISpSelectModuleIntentBuilder>? configure = null)
        {
            return module.SpSelect(builder =>
            {
                builder.SetSchema(schema);
                builder.SetName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpSelect(this IEntityModuleBuilder module, string name, Action<ISpSelectModuleIntentBuilder>? configure = null)
        {
            return module.SpSelect(builder =>
            {
                builder.SetName(name);
                configure?.Invoke(builder);
            });
        }
        public static IPackageToken SpSelect(this IEntityModuleBuilder module, Action<ISpSelectModuleIntentBuilder>? configure = null)
        {
            var token = module.PackageTokenProvider.CreateToken();
            module.Services.TryAddTransient<ISpSelectModuleIntentBuilder, SpSelectModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ISpSelectModuleIntentBuilder>();
                configure?.Invoke(builder);
                builder.Services.AddSingleton(token);
                return builder.Build();
            });
            return token;
        }
    }
}
