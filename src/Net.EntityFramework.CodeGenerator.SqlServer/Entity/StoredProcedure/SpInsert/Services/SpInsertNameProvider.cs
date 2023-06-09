using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpInsertNameProvider : IStoredProcedureNameProvider
    {
        private readonly IMutableEntityType _mutableEntity; 
        public SpInsertNameProvider(IMutableEntityType mutableEntity)
        {
            _mutableEntity = mutableEntity;  
        }
        public string Get()
        {
            var name = _mutableEntity.GetTableName();
             
            return $"usp_Insert_{name}";
        }
    }
}
