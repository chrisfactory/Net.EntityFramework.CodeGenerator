using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IEntityModuleBuilder SpSelect(this IEntityModuleBuilder module)
        {
            module.Services.TryAddTransient<ISpSelectModuleIntentBuilder, SpSelectModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ISpSelectModuleIntentBuilder>();
                return builder.Build();
            });
            return module;
        }
    }
}
