using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EntityFramework.CodeGenerator.Core
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCodeGenerator(this IServiceCollection services, Action<ICodeGeneratorBuilder> builder)
        {
            services.TryAddTransient<ICodeGeneratorBuilder, CodeGeneratorBuilder>();

            services.AddSingleton(p =>
            {
                var b = p.GetRequiredService<ICodeGeneratorBuilder>();
                builder.Invoke(b);
                return b.Build();
            });
            return services;
        }
    }
}
