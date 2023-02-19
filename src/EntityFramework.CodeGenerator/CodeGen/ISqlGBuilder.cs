using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public interface ISqlGBuilder 
    {
        IServiceCollection Services { get; }
        ICodeGenerator Build();
    }
}
