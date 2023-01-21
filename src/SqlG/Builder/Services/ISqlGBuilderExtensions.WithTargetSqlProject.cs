using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public  static partial class ISqlGBuilderExtensions
    { 
        public static ISqlGBuilder WithTargetSqlProject(this ISqlGBuilder services,string name)
        {
            //services.Services.AddSingleton(p =>
            //{
            //    var b = p.GetRequiredService<IEntityStrategyBuilder>();
            //    b.Services.AddSingleton<IEntity>(p => new EntityFromType<TEntity>());
            //    builder(p, b);
            //    return b.Build();
            //});
            return services;
        }
    }
}
