using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class ISqlGenModelBuilderExtensions
    {
        public static ISqlGenModelBuilder AllIndexs(this ISqlGenModelBuilder genBuilder) 
        {
            return genBuilder.AddGenActionBuilder<ICreateIndexsBuilder, CreateIndexsBuilder>();
        }
    }

    public interface ICreateIndexsBuilder : ISqlGenActionBuilder
    {
    }

    internal class CreateIndexsBuilder : SqlGenActionBuilder, ICreateIndexsBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            Services.AddTransient<ICreateIndexBuilder, CreateIndexBuilder>();
            Services.AddSingleton<ISqlGenActionProvider, CreateIndexsSqlGenActionProvider>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ISqlGenActionProvider>();
        }
    }

    public interface ICreateIndexSqlGenActionProvider : ISqlGenActionProvider
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
        public IEnumerable<ISqlGenAction> Get()
        {
            return new List<ISqlGenAction>();
        }

        public ICreateIndexBuilder GetIndexBuilder()
        {
            return _provider.GetRequiredService<ICreateIndexBuilder>();
        }
    }
}
