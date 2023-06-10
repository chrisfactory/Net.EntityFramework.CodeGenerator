using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public interface ISpUpdateParametersProvider
    {
        IReadOnlyCollection<IEntityColumn> GetParameters();
        IReadOnlyCollection<IEntityColumn> GetUpdate();
        IReadOnlyCollection<IEntityColumn> GetOutput();
        IReadOnlyCollection<IEntityColumn> GetWhere();
    }
    public class SpUpdateParametersProvider : ISpUpdateParametersProvider
    {
        private readonly IReadOnlyCollection<IEntityColumn> _parameterColumns;
        private readonly IReadOnlyCollection<IEntityColumn> _updateColumns;
        private readonly IReadOnlyCollection<IEntityColumn> _outputColumns;
        private readonly IReadOnlyCollection<IEntityColumn> _whereColumns;
        public SpUpdateParametersProvider(IDbContextModelContext context, IMutableEntityType mutableEntity)
        {
            var entity = context.GetEntity(mutableEntity);
            _parameterColumns = entity.AllColumns;
            _updateColumns = entity.UpdatableColumns;
            _outputColumns = entity.AllColumns;
            _whereColumns = entity.PrimaryKeys;
        }


        public IReadOnlyCollection<IEntityColumn> GetParameters()
        {
            return _parameterColumns;
        }
        public IReadOnlyCollection<IEntityColumn> GetUpdate()
        {
            return _updateColumns;
        }
        public IReadOnlyCollection<IEntityColumn> GetOutput()
        {
            return _outputColumns;
        }
        public IReadOnlyCollection<IEntityColumn> GetWhere()
        {
            return _whereColumns;
        }

    }
}
