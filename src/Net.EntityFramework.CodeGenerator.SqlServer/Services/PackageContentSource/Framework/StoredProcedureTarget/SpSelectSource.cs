using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SpSelectSource : ISpSelectCodeGeneratorSource
    {
        public SpSelectSource(IPackageScope scope)
        {
            Scope = scope;

            var _scope = (ITablePackageScope)scope;
            var _model = _scope.DbContextModel;
            var fullTableName = _scope.EntityModel.GetTableFullName();
            var entity = _model.Entities.Single(e => e.TableFullName == fullTableName);
            var columns = entity.AllColumns.ToList();
            var keys = entity.PrimaryKeys.ToList();

        }
        public string Name { get; } = "Sp Select";
        public IPackageScope Scope { get; }
    }
}
