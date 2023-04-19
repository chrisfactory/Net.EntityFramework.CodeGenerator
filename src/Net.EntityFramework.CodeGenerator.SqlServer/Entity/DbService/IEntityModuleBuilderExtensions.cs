using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IEntityModuleBuilder DbService(this IEntityModuleBuilder module)
        {
            module.Services.TryAddSingleton<IDbServiceModuleIntentBuilder, DbServiceModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IDbServiceModuleIntentBuilder>();
                return (IBuilder<IPostBuildPackageModuleIntent>)builder;
            });
            return module;
        }
    }
}
