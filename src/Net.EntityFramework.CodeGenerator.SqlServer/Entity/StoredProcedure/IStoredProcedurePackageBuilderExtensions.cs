using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator;

namespace Microsoft.Extensions.DependencyInjection
{

    public interface IStoredProcedureEfCallerNameProvider
    {
        string Get();
    }

    internal class FixedStoredProcedureEfCallerNameProvider : IStoredProcedureEfCallerNameProvider
    {
        private readonly string _name;
        public FixedStoredProcedureEfCallerNameProvider(string name)
        {
            this._name = name;
        }
        public string? Get()
        {
            return _name;
        }
    }
    public interface IStoredProcedureNameProvider
    {
        string Get();
    }
    internal class FixedStoredProcedureNameProvider : IStoredProcedureNameProvider
    {
        private readonly string _name;
        public FixedStoredProcedureNameProvider(string name)
        {
            this._name = name;
        }
        public string? Get()
        {
            return _name;
        }
    }

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
        public static TBuilder SetSpName<TBuilder>(this TBuilder builder, string name)
            where TBuilder : IStoredProcedurePackageBuilder
        {
             builder.Services.AddSingleton<IStoredProcedureNameProvider>(new FixedStoredProcedureNameProvider(name));
            return builder;
        }
        public static TBuilder SetSpSchema<TBuilder>(this TBuilder builder, string? schema)
           where TBuilder : IStoredProcedurePackageBuilder
        {
            builder.Services.AddSingleton<IStoredProcedureSchemaProvider>(new FixedStoredProcedureSchemaProvider(schema));
            return builder;
        }
        public static TBuilder SetEFCallerName<TBuilder>(this TBuilder builder, string name)
          where TBuilder : IStoredProcedurePackageBuilder
        {
            builder.Services.AddSingleton<IStoredProcedureEfCallerNameProvider>(new FixedStoredProcedureEfCallerNameProvider(name));
            return builder;
        }
    }
}
