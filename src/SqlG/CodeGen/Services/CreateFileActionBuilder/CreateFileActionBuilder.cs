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

        public ISqlGenAction Build()
        {
            Services.AddSingleton<ISqlGenAction, CreateFileAction>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenAction>();
        }
    }
}
