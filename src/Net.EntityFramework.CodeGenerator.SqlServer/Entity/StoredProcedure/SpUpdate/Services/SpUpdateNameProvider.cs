using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpUpdateNameProvider : IStoredProcedureNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        private readonly ISpUpdateParametersProvider _parametetersProvider;
        public SpUpdateNameProvider(
            IMutableEntityType mutableEntity,
            ISpUpdateParametersProvider parametersProvider)
        {
            _mutableEntity = mutableEntity;
            _parametetersProvider = parametersProvider;

        }
        public string Get()
        {

            var name = _mutableEntity.GetTableName();

            string by = string.Empty;
            string all = string.Empty;

            var parameters = _parametetersProvider.GetWhere();
            if (parameters == null || parameters.Count == 0)
                all = "All";
            else
            {
                by += "By";
                foreach (var parameter in parameters)
                    by += parameter.ColumnName;
            }


            return $"usp_Update_{all}{name}{by}"; 
        }
    }
}
