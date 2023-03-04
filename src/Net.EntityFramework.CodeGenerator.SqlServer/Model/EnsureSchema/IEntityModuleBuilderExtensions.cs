using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IModelModuleBuilder EnsureSchemas(this IModelModuleBuilder module)
        {
            module.Services.TryAddTransient<IEnsureSchemaModuleIntentBuilder, EnsureSchemaModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IEnsureSchemaModuleIntentBuilder>();
                return builder.Build();
            });
            return module;
        }
    }
}
