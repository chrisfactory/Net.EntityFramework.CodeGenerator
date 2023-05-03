using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleIntent
    {
        IPackageIdentity Identity { get; }
        IPackageContentSource ContentSource { get; }
        IEnumerable<IPackageIntent> Intents { get; }
    }


    public abstract class PackageModuleIntentBuilder<TSource, TSourceImplementation> : IBuilder<IPackageModuleIntent>
       where TSource : class, IPackageContentSource
       where TSourceImplementation : class, TSource
    {
        public PackageModuleIntentBuilder(IModuleStack moduleStack)
        {
            Services = moduleStack.BaseStack;
            Services.AddSingleton<TSource, TSourceImplementation>();
            Services.AddSingleton<IPackageContentSource>(p => p.GetRequiredService<TSource>()); 
        } 
        public IServiceCollection Services { get; }

        public abstract IPackageModuleIntent Build();
    }
}