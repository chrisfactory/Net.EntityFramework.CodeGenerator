using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
using System.Collections.Generic;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EfDbContextExtensionSpSelectPackageContentProvider : IIntentContentProvider
    {
        private readonly ISpSelectCodeGeneratorSource _source;
        public EfDbContextExtensionSpSelectPackageContentProvider(ISpSelectCodeGeneratorSource src)
        {
            _source = src;
        }

        public IEnumerable<IContent> Get()
        {
            yield return new EFStoredProcedureCaller(_source);
        }


        private class EFStoredProcedureCaller : IDotNetContentCodeSegment
        {
            private readonly ISpSelectCodeGeneratorSource _source;
            public EFStoredProcedureCaller(ISpSelectCodeGeneratorSource source)
            {
                _source = source;
                var usingType = new List<Type>() { typeof(RelationalQueryableExtensions), source.EntityTable.EntityType.ClrType };
                if (!_source.IsSelfDbContext)
                    usingType.Add(_source.DbContextType);
                if (_source.ResultSet == SelectResultSets.Select)
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
                string typeName = _source.EntityTable.EntityType.ClrType.Name;
                var spSchema = _source.Schema;
                var spName = _source.Name;
                var primaryKeys = _source.PrimaryKeys;

                var methodName = spName;

                var extTypeName = _source.DbContextType.Name;
                var methodParameters = string.Empty;
                var spParameters = string.Empty;
                if (primaryKeys != null)
                {
                    string sep = string.Empty; ;
                    foreach (var parameter in primaryKeys)
                    {
                        methodParameters += $",  {parameter.PropertyType}  {parameter.PropertyName}";
                        spParameters += $"  {sep}@{parameter.ColumnName} = {{{parameter.PropertyName}}}";
                        sep = ",";
                    }
                }


                var resultType = GetResultType(_source.ResultSet, typeName);

                builder.AppendLine($"public static {resultType} {methodName}(this {extTypeName} dbContext{methodParameters})");
                builder.AppendLine($"=> dbContext");
                builder.AppendLine($"   .Set<{typeName}>()");
                builder.AppendLine($"   .FromSql($\"EXECUTE [{spSchema}].[{spName}] {spParameters}\")");
                BuildResultSet(builder, _source.ResultSet); 
                builder.AppendLine();

                builder.AppendLine($"public static async Task<{resultType}> {methodName}Async(this {extTypeName} dbContext{methodParameters})");
                builder.AppendLine($"=> (await dbContext");
                builder.AppendLine($"   .Set<{typeName}>()");
                builder.AppendLine($"   .FromSql($\"EXECUTE [{spSchema}].[{spName}] {spParameters}\")");
                BuildAsyncResultSet(builder, _source.ResultSet);
                builder.AppendLine();
            }

            private string GetResultType(SelectResultSets resultSet, string typeName)
            {
                switch (resultSet)
                {
                    case SelectResultSets.Select:
                        return $"IEnumerable<{typeName}>";
                    case SelectResultSets.SelectSingle:
                    case SelectResultSets.SelectFirst:
                        return $"{typeName}";
                    case SelectResultSets.SelectSingleOrDefault:
                    case SelectResultSets.SelectFirstOrDefault:
                        return $"{typeName}?";
                    default:
                        throw new NotImplementedException($"{resultSet}");
                }
            }
            private void BuildResultSet(ICodeBuilder builder, SelectResultSets resultSet)
            {
                switch (resultSet)
                {
                    case SelectResultSets.Select:
                        {
                            builder.AppendLine($"   .AsEnumerable();"); 
                        }
                        break;
                    case SelectResultSets.SelectSingle:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .Single();");
                        }
                        break;
                    case SelectResultSets.SelectFirst:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .First();");
                        }
                        break;
                    case SelectResultSets.SelectSingleOrDefault:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .SingleOrDefault();");
                        }
                        break;
                    case SelectResultSets.SelectFirstOrDefault:
                        {
                            builder.AppendLine($"   .AsEnumerable()");
                            builder.AppendLine($"   .FirstOrDefault();");
                        }
                        break;
                    default:
                        throw new NotImplementedException($"{resultSet}");
                }
            }
            private void BuildAsyncResultSet(ICodeBuilder builder, SelectResultSets resultSet)
            {
                switch (resultSet)
                {
                    case SelectResultSets.Select:
                        {
                            builder.AppendLine($"   .ToListAsync());");
                        }
                        break;
                    case SelectResultSets.SelectSingle:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .Single();");
                        }
                        break;
                    case SelectResultSets.SelectFirst:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .First();");
                        }
                        break;
                    case SelectResultSets.SelectSingleOrDefault:
                        {
                            builder.AppendLine($"   .ToListAsync())");
                            builder.AppendLine($"   .SingleOrDefault();");
                        }
                        break;
                    case SelectResultSets.SelectFirstOrDefault:
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