using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public interface ISpInsertParametersProvider
    {
        IReadOnlyCollection<IEntityColumn> GetParameters();
        IReadOnlyCollection<IEntityColumn> GetOutput();
    }
    public class SpInsertParametersProvider : ISpInsertParametersProvider
    {
        private readonly IReadOnlyCollection<IEntityColumn> _insertColumns;
        private readonly IReadOnlyCollection<IEntityColumn> _outputColumns;
        public SpInsertParametersProvider(IDbContextModelContext context, IMutableEntityType mutableEntity)
        {
            var entity = context.GetEntity(mutableEntity);
            _insertColumns = entity.InsertColumns;
            _outputColumns = entity.AllColumns;
        }


        public IReadOnlyCollection<IEntityColumn> GetParameters()
        {
            return _insertColumns;
        }
        public IReadOnlyCollection<IEntityColumn> GetOutput()
        {
            return _outputColumns;
        }

    }
}
