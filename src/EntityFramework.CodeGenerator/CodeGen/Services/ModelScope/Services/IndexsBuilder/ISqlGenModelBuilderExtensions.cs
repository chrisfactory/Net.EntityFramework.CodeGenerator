using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder Indexs(this ISqlGenModelBuilder genBuilder) 
        {
            return genBuilder.AddGenActionBuilder<ICreateIndexsBuilder, CreateIndexsBuilder>();
        }
    }

    public interface ICreateIndexsBuilder : IActionBuilder
    {
    }

    internal class CreateIndexsBuilder : ActionBuilder, ICreateIndexsBuilder
    {

        public override IActionProvider Build()
        {
            Services.AddTransient<ICreateIndexBuilder, CreateIndexBuilder>();
            Services.AddSingleton<IActionProvider, CreateIndexsSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }

    public interface ICreateIndexSqlGenActionProvider : IActionProvider
    {
        ICreateIndexBuilder GetIndexBuilder();
    }
    internal class CreateIndexsSqlGenActionProvider : ICreateIndexSqlGenActionProvider
    {
        private readonly IServiceProvider _provider;
        public CreateIndexsSqlGenActionProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IEnumerable<IAction> Get()
        {
            return new List<IAction>();
        }

        public ICreateIndexBuilder GetIndexBuilder()
        {
            return _provider.GetRequiredService<ICreateIndexBuilder>();
        }
    }
}
