using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpSelectNameProvider : IStoredProcedureNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        private readonly ISpSelectParametersProvider _parametetersProvider;
        public SpSelectNameProvider(
            IMutableEntityType mutableEntity,
            ISpSelectParametersProvider parametersProvider)
        {
            _mutableEntity = mutableEntity;
            _parametetersProvider = parametersProvider;

        }
        public string Get()
        {
            var name = _mutableEntity.GetTableName();

            string by = string.Empty;
            string all = string.Empty;

            var parameters = _parametetersProvider.GetParameters();
            if (parameters == null || parameters.Count == 0)
                all = "All";
            else
            {
                by += "By";
                foreach (var parameter in parameters)
                    by += parameter.ColumnName;
            }


            return $"usp_Select_{all}{name}{by}";
        }
    }
}
