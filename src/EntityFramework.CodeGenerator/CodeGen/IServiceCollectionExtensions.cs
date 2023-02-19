using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection.Emit;

namespace EntityFramework.CodeGenerator
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerFileGenerator(this IServiceCollection services, Action<ModelBuilder> modelBuilder)
        {
            return services.AddSqlServerFileGenerator(null, modelBuilder);
        }
        public static IServiceCollection AddSqlServerFileGenerator(this IServiceCollection services, Action<ISqlGBuilder>? builder, Action<ModelBuilder> modelBuilder)
        {
            return services.AddSqlServerFileGenerator<SelfHostDbContext>(b =>
            {
                b.Services.AddSingleton(modelBuilder);
                builder?.Invoke(b);
            }); ;
        }

        public static IServiceCollection AddSqlServerFileGenerator<TDbContext>(this IServiceCollection services, Action<ISqlGBuilder>? builder)
             where TDbContext : DbContext
        {
            services.TryAddTransient<ISqlGBuilder, SqlGBuilder>();

            services.AddSingleton(p =>
            {
                var b = p.GetRequiredService<ISqlGBuilder>();
                b.Services.AddDbContext<TDbContext>(options => options.UseSqlServer());
                b.Services.AddSingleton(pb => (DbContext)pb.GetRequiredService<TDbContext>());
                builder?.Invoke(b);
                return b.Build();
            });
            return services;
        }






        //in DesignTimeDbContextFactory => IDesignTimeDbContextFactory
        //public static TDbContext UseSqlServerFileGenerator<TDbContext>(this TDbContext dbContext, Action<ISqlGBuilder<TDbContext>> builder)
        //    where TDbContext : DbContext
        //{

        //    return dbContext;
        //}


        //public static IServiceProvider UseSqlServerFileGenerator<TDbContext>(this IServiceProvider provider, Action<ISqlGBuilder<TDbContext>> builder)
        //   where TDbContext : DbContext
        //{
        //    var dbContext = provider.GetRequiredService<IDbContextFactory<TDbContext>>();

        //    return provider;
        //}

        ////Withous EF
        //public static IServiceProvider UseSqlServerFileGenerator(this IServiceProvider provider, Action<ISqlGBuilder> builder, Action<ModelBuilder> modelBuilder)
        //{
        //    var dbContext = provider.GetRequiredService<IDbContextFactory<SelfHostDbContext>>();
        //    return provider;
        //}





    }

    public static class SqlQerverFileGenerator
    {
        //public static void UseSqlServerFileGenerator(Action<ISqlGBuilder> builder, Action<ModelBuilder> modelBuilder)
        //{
        //    var services = new ServiceCollection();
        //    services.AddSqlServerFileGenerator();
        //    var provider = services.BuildServiceProvider();
        //    provider.UseSqlServerFileGenerator(builder, modelBuilder);
        //}
    }
}
