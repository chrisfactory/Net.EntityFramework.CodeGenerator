using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public interface ISpSelectParametersProvider
    {
        IReadOnlyCollection<IEntityColumn> GetParameters();
    }
    public class SpSelectPrimaryKeysParameters : ISpSelectParametersProvider
    {
        private readonly IReadOnlyCollection<IEntityColumn> _PrimaryKeys;
        public SpSelectPrimaryKeysParameters(IDbContextModelContext context, IMutableEntityType mutableEntity)
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
