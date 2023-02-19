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


    public interface ISqlGenActionBuilder
    {
        IReadOnlyCollection<IRequiredDependencyService> RequiredDependencies { get; }
        IServiceCollection Services { get; }
        ISqlGenActionProvider Build();
    }

    internal abstract class SqlGenActionBuilder : ISqlGenActionBuilder
    {
        public SqlGenActionBuilder(params IRequiredDependencyService[] dependencies)
        {
            RequiredDependencies = dependencies?.ToList() ?? new List<IRequiredDependencyService>();
            Services = new ServiceCollection();
            Services.AddTransient<ICreateFileActionBuilder, CreateFileActionBuilder>();
            Services.AddSingleton<ISqlFileInfoFactory, GetSqlFileInfoFactory>(); 
            LoadDefaultServices();
        }
        protected virtual void LoadDefaultServices() { }



        public IReadOnlyCollection<IRequiredDependencyService> RequiredDependencies { get; }
        public IServiceCollection Services { get; }
        public abstract ISqlGenActionProvider Build();
    }

    public interface IRequiredDependencyService
    {

    }













    public interface ISqlGenActionBuilderProvider
    {
        IReadOnlyCollection<ISqlGenActionBuilder> Get();
    }
    internal class SqlGenActionBuilderProvider : ISqlGenActionBuilderProvider
    {
        private readonly IServiceProvider _provider;
        public SqlGenActionBuilderProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IReadOnlyCollection<ISqlGenActionBuilder> Get()
        {
            return new List<ISqlGenActionBuilder>(_provider.GetServices<ISqlGenActionBuilder>());
        }
    }
}
