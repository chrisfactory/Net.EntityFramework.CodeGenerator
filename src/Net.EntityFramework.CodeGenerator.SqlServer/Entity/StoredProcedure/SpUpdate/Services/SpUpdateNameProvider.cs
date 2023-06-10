using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpUpdateNameProvider : IStoredProcedureNameProvider
    {
        private readonly IMutableEntityType _mutableEntity; 
        public SpUpdateNameProvider(IMutableEntityType mutableEntity)
        {
            _mutableEntity = mutableEntity;  
        }
        public string Get()
        {
            var name = _mutableEntity.GetTableName();
             
            return $"usp_Update_{name}";
        }
    }
}
