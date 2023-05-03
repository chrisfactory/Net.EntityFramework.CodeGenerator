using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken CreateTable(this IEntityModuleBuilder module)
        {
            var token = module.PackageTokenProvider.CreateToken();
            module.Services.TryAddTransient<ICreateTableModuleIntentBuilder, CreateTableModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ICreateTableModuleIntentBuilder>();
                builder.Services.AddSingleton(token);
                return builder.Build();
            });
            return token;
        }
    }
}
