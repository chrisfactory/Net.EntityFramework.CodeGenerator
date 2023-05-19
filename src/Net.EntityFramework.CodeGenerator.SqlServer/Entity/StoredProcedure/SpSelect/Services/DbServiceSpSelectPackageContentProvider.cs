using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSpSelectPackageContentProvider : IIntentContentProvider
    {
        private readonly ISpSelectCodeGeneratorSource _source;
        public DbServiceSpSelectPackageContentProvider(ISpSelectCodeGeneratorSource src)
        {
            _source = src;
        }

        public IEnumerable<IContent> Get()
        {
            yield return new SpSelectStoredProcedureInfos(
                _source.Name,
                _source.PrimaryKeys);
        }
    }
     
    internal class SpSelectStoredProcedureInfos : IStoredProcedureInfos
    {
        public SpSelectStoredProcedureInfos(string name, IReadOnlyCollection<IEntityColumn> parameters)
        {
            StoredProcedureName = name;
            Parameters = parameters;
        }
        public string StoredProcedureName { get; }
        public IReadOnlyCollection<IEntityColumn> Parameters { get; }


    }
}
