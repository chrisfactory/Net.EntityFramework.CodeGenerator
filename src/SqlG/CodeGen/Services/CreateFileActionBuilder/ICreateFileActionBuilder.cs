using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal interface ICreateFileActionBuilder
    {
        IServiceCollection Services { get; }
        ISqlGAction Build();
    }
}
