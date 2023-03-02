using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    public static partial class ICodeGeneratorBuilderExtensions
    {
        public static ICodeGeneratorBuilder UseSefModeDbContext(this ICodeGeneratorBuilder builder, Action<ModelBuilder> modelBuilder, Action<DbContextOptionsBuilder>? optionsAction = null)
        {
            builder.Services.AddSingleton(modelBuilder);
            return builder.UseDbContext<SelfDbContext>(optionsAction);
        }
        public static ICodeGeneratorBuilder UseDbContext<TDbContext>(this ICodeGeneratorBuilder builder, Action<DbContextOptionsBuilder>? optionsAction = null)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContext<SelfDbContext>(optionsAction);
            builder.Services.AddSingleton(pb => (DbContext)pb.GetRequiredService<TDbContext>());
            return builder;
        }
    }
}
