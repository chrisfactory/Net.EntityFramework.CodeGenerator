using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken CreateSequences(this IModelModuleBuilder module)
        {
            var token = module.PackageTokenProvider.CreateToken();
            module.Services.TryAddTransient<ISequencesModuleIntentBuilder, SequencesModuleIntentBuilder>();

            module.Services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<ISequencesModuleIntentBuilder>();
                builder.Services.AddSingleton(token);
                return builder.Build();
            });
            return token;
        }
    }
}
