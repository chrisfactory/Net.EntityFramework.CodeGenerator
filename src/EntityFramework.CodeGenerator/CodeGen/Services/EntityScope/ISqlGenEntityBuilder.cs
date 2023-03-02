using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public interface ISqlGenEntityBuilder
    {
        IServiceCollection Services { get; }
        ISqlGenActionBuilderProvider Build();
    }
    internal class SqlGenEntityBuilder : ISqlGenEntityBuilder
    {
        public SqlGenEntityBuilder()
        {
            Services = new ServiceCollection();
        }

        public IServiceCollection Services { get; }

        public ISqlGenActionBuilderProvider Build()
        {
            Services.AddSingleton<ISqlGenActionBuilderProvider, SqlGenActionBuilderProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionBuilderProvider>();
        }
    }


    public interface IActionBuilder
    {
        IReadOnlyCollection<IRequiredDependencyService> RequiredDependencies { get; }
        IServiceCollection Services { get; }
        IActionProvider Build();
    }

    internal abstract class ActionBuilder : IActionBuilder
    {
        public ActionBuilder(params IRequiredDependencyService[] dependencies)
        {
            RequiredDependencies = dependencies?.ToList() ?? new List<IRequiredDependencyService>();
            Services = new ServiceCollection();
            Services.AddTransient<ICreateFileActionBuilder, CreateFileActionBuilder>();
            Services.AddSingleton<IFileInfoFactory, FileInfoFactory>(); 
            LoadDefaultServices();
        }
        protected virtual void LoadDefaultServices() { }



        public IReadOnlyCollection<IRequiredDependencyService> RequiredDependencies { get; }
        public IServiceCollection Services { get; }
        public abstract IActionProvider Build();
    }

    public interface IRequiredDependencyService
    {

    }



    public interface ISqlGenActionBuilderProvider
    {
        IReadOnlyCollection<IActionBuilder> Get();
    }
    internal class SqlGenActionBuilderProvider : ISqlGenActionBuilderProvider
    {
        private readonly IServiceProvider _provider;
        public SqlGenActionBuilderProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IReadOnlyCollection<IActionBuilder> Get()
        {
            return new List<IActionBuilder>(_provider.GetServices<IActionBuilder>());
        }
    }
}
