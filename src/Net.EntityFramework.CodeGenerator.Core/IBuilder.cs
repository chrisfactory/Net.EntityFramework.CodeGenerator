using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IBuilder<T>
    {
        IServiceCollection Services { get; }
        T Build();
    }
}
