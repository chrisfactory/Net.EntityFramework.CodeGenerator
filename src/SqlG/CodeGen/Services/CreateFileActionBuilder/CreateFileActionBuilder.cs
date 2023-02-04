using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal class CreateFileActionBuilder : ICreateFileActionBuilder
    {
        public CreateFileActionBuilder()
        {
            Services = new ServiceCollection();
        }

        public IServiceCollection Services { get; }

        public ISqlGAction Build()
        {
            Services.AddSingleton<ISqlGAction, CreateFileAction>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGAction>();
        }
    }
}
