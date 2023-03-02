using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IEntityModuleBuilder CreateTable(this IEntityModuleBuilder module)
        {
            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ICreateTableModuleIntentBuilder>();
                return builder.Build();
            });
            return module;
        }
    }
}
