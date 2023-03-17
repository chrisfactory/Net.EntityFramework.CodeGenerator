using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSpSelectPackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public DbServiceSpSelectPackageContentProvider(IPackageScope scope)
        {
            _scope = (ITablePackageScope)scope;
            _model = _scope.DbContextModel;
        }

        public IEnumerable<IPackageContent> Get()
        {
            throw new NotImplementedException();
        }
    }
}
