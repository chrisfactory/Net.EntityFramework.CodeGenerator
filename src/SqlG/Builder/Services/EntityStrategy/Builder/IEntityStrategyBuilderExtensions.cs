using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SqlG
{
    public static class IEntityStrategyBuilderExtensions
    {
        public static IEntityStrategyBuilder UseEntityMapClassEntensions(this IEntityStrategyBuilder builder)
        {
            builder.Services.AddSingleton<IEntityMapGenerator, EntityMapClassEntensions>();
            return builder;
        }

        public static IEntityStrategyBuilder UseEntityMapClassPartial(this IEntityStrategyBuilder builder)
        {
            builder.Services.AddSingleton<IEntityMapGenerator, EntityMapPartial>();
            return builder;
        }
    }
}
