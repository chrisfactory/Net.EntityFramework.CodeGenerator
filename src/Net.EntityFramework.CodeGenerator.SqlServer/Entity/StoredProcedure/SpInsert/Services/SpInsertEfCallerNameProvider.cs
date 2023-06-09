using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpInsertEfCallerNameProvider : IStoredProcedureEfCallerNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        public SpInsertEfCallerNameProvider(IMutableEntityType mutableEntity)
        {
            _mutableEntity = mutableEntity;
        }
        public string Get()
        {
            var name = _mutableEntity.ClrType.Name;

            return $"Insert{name}";
        }
    }
}
