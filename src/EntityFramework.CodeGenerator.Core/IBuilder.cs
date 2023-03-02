using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IBuilder<T>
    {
        IServiceCollection Services { get; }
        T Build();
    }
}
