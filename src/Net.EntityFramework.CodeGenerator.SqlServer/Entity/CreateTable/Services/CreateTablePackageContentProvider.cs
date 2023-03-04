using Net.EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public CreateTablePackageContentProvider(IPackageScope scope)
        {
            _scope = (ITablePackageScope)scope;
            _model = _scope.DbContextModel;
        }

        public IEnumerable<IPackageContent> Get()
        {
            var schema = _scope.EntityModel.GetSchema();
            var tableName = _scope.EntityModel.GetTableName();
            foreach (var cmd in _model.CreateTableIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Name == tableName)
                    yield return new CommandTextSegment(cmd.Command.CommandText);
            }
        }
    }
}
