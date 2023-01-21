using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public interface IEntityStrategyBuilder
    {
        IServiceCollection Services { get; }
        IEntityStrategy Build();
    }
}
