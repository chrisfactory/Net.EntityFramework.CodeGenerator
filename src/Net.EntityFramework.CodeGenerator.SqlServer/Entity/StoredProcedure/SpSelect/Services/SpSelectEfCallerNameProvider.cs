using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class SpSelectEfCallerNameProvider : IStoredProcedureEfCallerNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        private readonly IResultSet _resultSet;
        private readonly ISpSelectParametersProvider _parametetersProvider;
        public SpSelectEfCallerNameProvider(
            IMutableEntityType mutableEntity,
            IResultSet resultSet,
            ISpSelectParametersProvider parametersProvider)
        {
            _mutableEntity = mutableEntity;
            _resultSet = resultSet;
            _parametetersProvider = parametersProvider;
        }
        public string Get()
        {
            var name = _mutableEntity.ClrType.Name;

            string by = string.Empty;
            string scope = _resultSet.ResultSet == ResultSets.None ? string.Empty : $"{_resultSet.ResultSet}";

            var parameters = _parametetersProvider.GetParameters();
            if (parameters == null || parameters.Count == 0)
            {
                if (_resultSet.ResultSet != ResultSets.None)
                    scope = "All";
            }
            else
            {
                by += "By";
                foreach (var parameter in parameters)
                    by += parameter.PropertyName;
            }
            return $"Get{scope}{name}{by}";
        }
    }
}
