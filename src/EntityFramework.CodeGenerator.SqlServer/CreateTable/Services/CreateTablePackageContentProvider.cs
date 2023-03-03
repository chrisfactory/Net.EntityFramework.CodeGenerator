using EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public CreateTablePackageContentProvider(IPackageScope scope, IDbContextModelExtractor model)
        {
            _scope = (ITablePackageScope)scope;
            _model = model;
        }

        public IEnumerable<IPackageContent> Get()
        {
            var schema = _scope.MetaData.GetSchema();
            var tableName = _scope.MetaData.GetTableName();
            foreach (var cmd in _model.CreateTableIntents)
            {
                if (cmd.Operation.Schema == schema && cmd.Operation.Name == tableName)
                    yield return new CommandTextSegment(cmd.Command.CommandText);
            }
        }
    }
}
