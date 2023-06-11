using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public interface ISpDeleteParametersProvider
    {
        IReadOnlyCollection<IEntityColumn> GetParameters();
    }
    public class SpDeleteParametersProvider : ISpDeleteParametersProvider
    {
        private readonly IReadOnlyCollection<IEntityColumn> _PrimaryKeys;
        public SpDeleteParametersProvider(IDbContextModelContext context, IMutableEntityType mutableEntity)
        {
            var entity = context.GetEntity(mutableEntity);
            _PrimaryKeys = entity.PrimaryKeys;
        }
        public IReadOnlyCollection<IEntityColumn> GetParameters()
        {
            return _PrimaryKeys;
        }
    }
}
