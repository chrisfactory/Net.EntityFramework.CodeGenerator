using EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexPackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public CreateIndexPackageContentProvider(IPackageScope scope)
        {
            _scope = (ITablePackageScope)scope;
            _model = _scope.DbContextModel;
        }

        public IEnumerable<IPackageContent> Get()
        {
            var schema = _scope.EntityModel.GetSchema();
            var tableName = _scope.EntityModel.GetTableName();
            foreach (var cmd in _model.CreateIndexIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Table == tableName)
                    yield return new CommandTextSegment(cmd.Command.CommandText);
            }
        }
    }
}
