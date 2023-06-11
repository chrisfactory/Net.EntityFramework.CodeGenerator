using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpDeleteNameProvider : IStoredProcedureNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        private readonly ISpDeleteParametersProvider _parametetersProvider;
        public SpDeleteNameProvider(
            IMutableEntityType mutableEntity,
            ISpDeleteParametersProvider parametersProvider)
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


            return $"usp_Delete_{all}{name}{by}";
        }
    }
}
