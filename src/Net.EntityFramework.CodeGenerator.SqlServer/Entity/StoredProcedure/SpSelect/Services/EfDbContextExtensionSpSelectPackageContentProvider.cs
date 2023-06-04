using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
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
                    string sep=string.Empty; ;
                    foreach (var parameter in primaryKeys)
                    {
                        methodParameters += $",  {parameter.PropertyType}  {parameter.PropertyName}";
                        spParameters += $"  {sep}@{parameter.ColumnName} = {{{parameter.PropertyName}}}";
                        sep = ",";
                    }
                }

                builder.AppendLine($"public static {typeName}? {methodName}(this {extTypeName} dbContext{methodParameters})");
                builder.AppendLine($"=> dbContext");
                builder.AppendLine($"   .Set<{typeName}>()");
                builder.AppendLine($"   .FromSql($\"EXECUTE [{spSchema}].[{spName}] {spParameters}\")");
                builder.AppendLine($"   .AsEnumerable()");
                builder.AppendLine($"   .SingleOrDefault();");
                builder.AppendLine();

                builder.AppendLine($"public static async Task<{typeName}?> {methodName}Async(this {extTypeName} dbContext{methodParameters})");
                builder.AppendLine($"=> (await dbContext");
                builder.AppendLine($"   .Set<{typeName}>()");
                builder.AppendLine($"   .FromSql($\"EXECUTE [{spSchema}].[{spName}] {spParameters}\")");
                builder.AppendLine($"   .ToListAsync())");
                builder.AppendLine($"   .SingleOrDefault();");
                builder.AppendLine();
            }
        }
    }
}