using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IStoredProcedureSchemaProvider
    {
        string? Get();
    }
    internal class FixedStoredProcedureSchemaProvider : IStoredProcedureSchemaProvider
    {
        private readonly string? _schema;
        public FixedStoredProcedureSchemaProvider(string? schema)
        {
            this._schema = schema;
        }
        public string? Get()
        {
            return _schema;
        }
    }

    internal class TablePackageProcedureSchemaProvider : IStoredProcedureSchemaProvider
    {
        private readonly string? _schema;
        public TablePackageProcedureSchemaProvider(IMutableEntityType entity)
        {
            this._schema = entity.GetSchema();
        }

        public string? Get()
        {
            return _schema;
        }
    }

    public class SPInfos
    {
        public string TableName { get; set; }
        public string TableFullName { get; set; }
        public IEntityTypeTable EntityTable { get; set; }
        public IReadOnlyCollection<IEntityColumn> ProjectionColumns { get; set; }
        public IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; set; }
    }
    public interface IStoredProcedureNameProvider
    {
        string Get(SPInfos infos);
    }
    internal class FixedStoredProcedureNameProvider : IStoredProcedureNameProvider
    {
        private readonly string _name;
        public FixedStoredProcedureNameProvider(string name)
        {
            this._name = name;
        }
        public string Get(SPInfos infos)
        {
            return _name;
        }
    }


    internal class SelectTableProcedureNameProvider : IStoredProcedureNameProvider
    {

        public string Get(SPInfos infos)
        {
            var tableName = infos.TableName;
            var keys = string.Empty;
            foreach (var key in infos.PrimaryKeys)
                keys += key.ColumnName;

            return $"Select{tableName}By{keys}";
        }

    }


    public static class IStoredProcedurePackageBuilderExtensions
    {
        public static TBuilder SetName<TBuilder>(this TBuilder builder, string name)
            where TBuilder : IStoredProcedurePackageBuilder
        {
            builder.Services.AddSingleton<IStoredProcedureNameProvider>(new FixedStoredProcedureNameProvider(name));
            return builder;
        }
        public static TBuilder SetSchema<TBuilder>(this TBuilder builder, string? schema)
           where TBuilder : IStoredProcedurePackageBuilder
        {
            builder.Services.AddSingleton<IStoredProcedureSchemaProvider>(new FixedStoredProcedureSchemaProvider(schema));
            return builder;
        }
    }
}
