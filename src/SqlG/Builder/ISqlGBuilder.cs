using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public interface ISqlGBuilder
    {
        IServiceCollection Services { get; }
        ISqlGenerator Build();
    }
}
