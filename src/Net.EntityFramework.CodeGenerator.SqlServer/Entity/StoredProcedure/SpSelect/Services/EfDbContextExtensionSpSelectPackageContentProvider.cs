using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public class EfDbContextExtensionSpSelectPackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelContext _context;
        private readonly IEntityTypeTable _entityTable;
        private readonly ISpSelectParametersProvider _parametersProvider;
        private readonly IStoredProcedureSchemaProvider _schemaProvider;
        private readonly IStoredProcedureNameProvider _spNameProvider;
        private readonly IStoredProcedureEfCallerNameProvider _callerNameProvider;
        private readonly IResultSet _resultSet;
        public EfDbContextExtensionSpSelectPackageContentProvider(
            IDbContextModelContext context,
            IMutableEntityType mutableEntity,
            ISpSelectParametersProvider parametersProvider,
            IStoredProcedureSchemaProvider schemaProvider,
            IStoredProcedureNameProvider spNameProvider,
            IStoredProcedureEfCallerNameProvider callerNameProvider,
            IResultSet resultSet)
        {
            _context = context;
            _parametersProvider = parametersProvider;
            _schemaProvider = schemaProvider;
            _spNameProvider = spNameProvider;
            _callerNameProvider = callerNameProvider;
            _entityTable = context.GetEntity(mutableEntity);
            _resultSet = resultSet;
        }

        public IEnumerable<IContent> Get()
        {
            yield return new EFStoredProcedureCaller(_context, _entityTable, _parametersProvider, _schemaProvider, _spNameProvider, _callerNameProvider, _resultSet);
        }


        private class EFStoredProcedureCaller : IDotNetContentCodeSegment
        {
            private readonly IDbContextModelContext _context;
            private readonly IEntityTypeTable _entityTable;
            private readonly ISpSelectParametersProvider _parametersProvider;
            private readonly IStoredProcedureSchemaProvider _schemaProvider;
            private readonly IStoredProcedureNameProvider _spNameProvider;
            private readonly IStoredProcedureEfCallerNameProvider _callerNameProvider;
            private readonly IResultSet _resultSet;
            public EFStoredProcedureCaller(
                IDbContextModelContext context,
                IEntityTypeTable entityTable,
                ISpSelectParametersProvider parametersProvider,
                IStoredProcedureSchemaProvider schemaProvider,
                IStoredProcedureNameProvider spNameProvider,
                IStoredProcedureEfCallerNameProvider callerNameProvider,
                IResultSet resultSet)
            {
                _context = context;
                _entityTable = entityTable;
                _parametersProvider = parametersProvider;
                _schemaProvider = schemaProvider;
                _spNameProvider = spNameProvider;
                _callerNameProvider = callerNameProvider;
                _resultSet = resultSet;

                var usingType = new List<Type>() { typeof(RelationalQueryableExtensions), _entityTable.EntityType.ClrType };
                if (!context.IsSelfDbContext)
                    usingType.Add(context.DbContextType);
                if (resultSet.ResultSet == ResultSets.None)
                    usingType.Add(typeof(IEnumerable<>));
                foreach (var item in usingType)
                {
                    if (!string.IsNullOrEmpty(item.Namespace))
                        Usings.Add(item.Namespace);
                }
            }
            public List<string> Usings { get; } = new List<string>();

            public void Build(ICodeBuilder builder)
            {
                string typeName = _entityTable.EntityType.ClrType.Name;

                var spSchema = _schemaProvider.Get();
                var spName = _spNameProvider.Get();
                var methodName = _callerNameProvider.Get();
                var parameters = _parametersProvider.GetParameters();


                var extTypeName = _context.DbContextType.Name;
                var methodParameters = string.Empty;
                var spParameters = string.Empty;
                if (parameters != null)
                {
                    string sep = string.Empty; ;
                    foreach (var parameter in parameters)
                    {
                        methodParameters += $", {parameter.PropertyType} {parameter.CallPropertyName}";
                        spParameters += $"{sep}@{parameter.ColumnName} = {{{parameter.CallPropertyName}}}";
                        sep = ",";
                    }
                }

                if (!string.IsNullOrEmpty(spSchema))
                    spSchema = $"[{spSchema}].";
                var resultType = GetResultType(_resultSet.ResultSet, typeName);

                builder.AppendLine($"public static {resultType} {methodName}(this {extTypeName} dbContext{methodParameters})");
                builder.AppendLine($"=> dbContext");
                builder.AppendLine($"   .Set<{typeName}>()");
                builder.AppendLine($"   .FromSql($\"EXECUTE {spSchema}[{spName}] {spParameters}\")");
                BuildResultSet(builder, _resultSet.ResultSet);
                builder.AppendLine();

                builder.AppendLine($"public static async Task<{resultType}> {methodName}Async(this {extTypeName} dbContext{methodParameters})");
                builder.AppendLine($"=> (await dbContext");
                builder.AppendLine($"   .Set<{typeName}>()");
                builder.AppendLine($"   .FromSql($\"EXECUTE {spSchema}[{spName}] {spParameters}\")");
                BuildAsyncResultSet(builder, _resultSet.ResultSet);
                builder.AppendLine();
            }

            private string GetResultType(ResultSets resultSet, string typeName)
            {
                switch (resultSet)
                {
                    case ResultSets.None:
                        return $"IEnumerable<{typeName}>";
                    case ResultSets.Single:
                    case ResultSets.First:
                        return $"{typeName}";
                    case ResultSets.SingleOrDefault:
                    case ResultSets.FirstOrDefault:
                        return $"{typeName}?";
                    default:
                        throw new NotImplementedException($"{resultSet}");
                }
            }
            private void BuildResultSet(ICodeBuilder builder, ResultSets resultSet)
            {
                switch (resultSet)
                {
                    case ResultSets.None:
                        {
                            builder.AppendLine($"   .AsEnumerable();");
                        }
                        break;
                    case ResultSets.Single:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .Single();");
                        }
                        break;
                    case ResultSets.First:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .First();");
                        }
                        break;
                    case ResultSets.SingleOrDefault:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .SingleOrDefault();");
                        }
                        break;
                    case ResultSets.FirstOrDefault:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .FirstOrDefault();");
                        }
                        break;
                    default:
                        throw new NotImplementedException($"{resultSet}");
                }
            }
            private void BuildAsyncResultSet(ICodeBuilder builder, ResultSets resultSet)
            {
                switch (resultSet)
                {
                    case ResultSets.None:
                        {
                            builder.AppendLine($"   .ToListAsync());");
                        }
                        break;
                    case ResultSets.Single:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .Single();");
                        }
                        break;
                    case ResultSets.First:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .First();");
                        }
                        break;
                    case ResultSets.SingleOrDefault:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .SingleOrDefault();");
                        }
                        break;
                    case ResultSets.FirstOrDefault:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .FirstOrDefault();");
                        }
                        break;
                    default:
                        throw new NotImplementedException($"{resultSet}");
                }
            }
        }
    }
}