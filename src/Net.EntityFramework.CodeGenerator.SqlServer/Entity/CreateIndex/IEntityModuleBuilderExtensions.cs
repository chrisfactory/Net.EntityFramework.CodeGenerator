using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IEntityModuleBuilder CreateIndex(this IEntityModuleBuilder module)
        {
            module.Services.TryAddTransient<ICreateIndexModuleIntentBuilder, CreateIndexModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ICreateIndexModuleIntentBuilder>();
                return builder.Build();
            });
            return module;
        }
    }
}
