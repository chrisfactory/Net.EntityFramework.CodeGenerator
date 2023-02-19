using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    internal interface ICreateFileActionBuilder
    {
        IServiceCollection Services { get; }
        ISqlGenAction Build();
    }
}
