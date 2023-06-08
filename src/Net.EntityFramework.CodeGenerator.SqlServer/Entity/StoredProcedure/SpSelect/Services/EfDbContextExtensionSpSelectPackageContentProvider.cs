using Microsoft.EntityFrameworkCore;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EfDbContextExtensionSpSelectPackageContentProvider : IIntentContentProvider
    {
        public EfDbContextExtensionSpSelectPackageContentProvider()
        {
        }

        public IEnumerable<IContent> Get()
        {
            yield return new EFStoredProcedureCaller();
        }


        private class EFStoredProcedureCaller : IDotNetContentCodeSegment
        {
            public EFStoredProcedureCaller()
            {
                //_source = source;
                //var usingType = new List<Type>() { typeof(RelationalQueryableExtensions), source.EntityTable.EntityType.ClrType };
                //if (!_source.IsSelfDbContext)
                //    usingType.Add(_source.DbContextType);
                //if (_source.ResultSet == ResultSets.None)
                //    usingType.Add(typeof(IEnumerable<>));
                //foreach (var item in usingType)
                //{
                //    if (!string.IsNullOrEmpty(item.Namespace))
                //        Usings.Add(item.Namespace);
                //}
            }
            public List<string> Usings { get; } = new List<string>();

            public void Build(ICodeBuilder builder)
            {
                //string typeName = _source.EntityTable.EntityType.ClrType.Name;
                //var spSchema = _source.Schema;
                //var spName = _source.Name;
                //var primaryKeys = _source.PrimaryKeys;

                //var methodName = spName;

                //var extTypeName = _source.DbContextType.Name;
                //var methodParameters = string.Empty;
                //var spParameters = string.Empty;
                //if (primaryKeys != null)
                //{
                //    string sep = string.Empty; ;
                //    foreach (var parameter in primaryKeys)
                //    {
                //        methodParameters += $",  {parameter.PropertyType}  {parameter.PropertyName}";
                //        spParameters += $"  {sep}@{parameter.ColumnName} = {{{parameter.PropertyName}}}";
                //        sep = ",";
                //    }
                //}


                //var resultType = GetResultType(_source.ResultSet, typeName);

                //builder.AppendLine($"public static {resultType} {methodName}(this {extTypeName} dbContext{methodParameters})");
                //builder.AppendLine($"=> dbContext");
                //builder.AppendLine($"   .Set<{typeName}>()");
                //builder.AppendLine($"   .FromSql($\"EXECUTE [{spSchema}].[{spName}] {spParameters}\")");
                //BuildResultSet(builder, _source.ResultSet);
                //builder.AppendLine();

                //builder.AppendLine($"public static async Task<{resultType}> {methodName}Async(this {extTypeName} dbContext{methodParameters})");
                //builder.AppendLine($"=> (await dbContext");
                //builder.AppendLine($"   .Set<{typeName}>()");
                //builder.AppendLine($"   .FromSql($\"EXECUTE [{spSchema}].[{spName}] {spParameters}\")");
                //BuildAsyncResultSet(builder, _source.ResultSet);
                //builder.AppendLine();
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