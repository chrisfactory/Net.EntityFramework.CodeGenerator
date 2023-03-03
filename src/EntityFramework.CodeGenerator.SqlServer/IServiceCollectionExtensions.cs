using EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerCodeGenerator(this IServiceCollection services, Action<ModelBuilder> modelBuilder)
        {
            return services.AddCodeGenerator(cg =>
            {
                cg.UseSefModeDbContext(modelBuilder, opt => { opt.UseSqlServer(); });
            });
        }
        public static IServiceCollection AddSqlServerCodeGenerator<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
             where TDbContext : DbContext
        {
            return services.AddCodeGenerator(cg =>
            {
                cg.UseDbContext<TDbContext>(optionsAction);
            });
        }
    }
}
