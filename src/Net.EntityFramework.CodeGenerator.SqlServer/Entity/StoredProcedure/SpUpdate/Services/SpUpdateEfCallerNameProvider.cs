using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpUpdateEfCallerNameProvider : IStoredProcedureEfCallerNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        public SpUpdateEfCallerNameProvider(IMutableEntityType mutableEntity)
        {
            _mutableEntity = mutableEntity;
        }
        public string Get()
        {
            var name = _mutableEntity.ClrType.Name;

            return $"Update{name}";
        }
    }
}
