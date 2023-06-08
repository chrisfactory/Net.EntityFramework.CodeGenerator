using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator;

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

 

    public static class IStoredProcedurePackageBuilderExtensions
    {
        public static TBuilder SetName<TBuilder>(this TBuilder builder, string name)
            where TBuilder : IStoredProcedurePackageBuilder
        {
          //  builder.Services.AddSingleton<IStoredProcedureNameProvider>(new FixedStoredProcedureNameProvider(name));
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
