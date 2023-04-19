using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServicePackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public DbServicePackageContentProvider(IPackageScope scope)
        {
            _scope = (ITablePackageScope)scope;
            _model = _scope.DbContextModel;
        }

        public IEnumerable<IPackageContent> Get()
        {
            yield return new CommandTextSegment("DbService");
        }
    }
}
