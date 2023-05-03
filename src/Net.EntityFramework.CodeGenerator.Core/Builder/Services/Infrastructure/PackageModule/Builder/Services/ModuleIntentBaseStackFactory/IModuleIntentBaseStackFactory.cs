using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{

    public interface IModuleStack
    {
        IServiceCollection BaseStack { get; }
    }
    public interface IModuleIntentBaseStackFactory
    {
        IModuleStack Create();
    }
}
