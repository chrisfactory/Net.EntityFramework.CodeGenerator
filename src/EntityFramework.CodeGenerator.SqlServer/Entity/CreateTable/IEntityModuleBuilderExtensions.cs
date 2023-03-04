using EntityFramework.CodeGenerator.Core;
using EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IEntityModuleBuilder CreateTable(this IEntityModuleBuilder module)
        {
            module.Services.TryAddTransient<ICreateTableModuleIntentBuilder, CreateTableModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ICreateTableModuleIntentBuilder>();
                return builder.Build();
            });
            return module;
        }
    }
}
