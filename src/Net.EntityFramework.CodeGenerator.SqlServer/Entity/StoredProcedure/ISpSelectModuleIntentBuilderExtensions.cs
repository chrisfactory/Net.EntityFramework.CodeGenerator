using Microsoft.EntityFrameworkCore;
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
        public TablePackageProcedureSchemaProvider(IPackageScope scope)
        {
            var _scope = (ITablePackageScope)scope;
            this._schema = _scope.EntityModel.GetSchema();
        }

        public string? Get()
        {
            return _schema;
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
        public string Get()
        {
            return _name;
        }
    }

    internal abstract class TablePackageProcedureNameProvider : IStoredProcedureNameProvider
    {
        private readonly string _name;
        public TablePackageProcedureNameProvider(IPackageScope scope)
        {
            var _scope = (ITablePackageScope)scope;
            var schemaName = _scope.EntityModel.GetSchemaQualifiedTableName();
            this._name = _scope.EntityModel.GetTableName();
        }

        public virtual string Get()
        {
            return _name;
        }
    }
    internal class SelectTableProcedureNameProvider : TablePackageProcedureNameProvider
    {
        public SelectTableProcedureNameProvider(IPackageScope scope) : base(scope)
        {
        }

        public override string Get()
        {
            return $"Select{base.Get()}By{GetKeyName()}";
        }

        private string GetKeyName()
        {
            return "Truc";
        }
    }


    public static class ISpSelectModuleIntentBuilderExtensions
    {
        public static TBuilder SetName<TBuilder>(this TBuilder builder, string name)
            where TBuilder : IStoredProcedureModuleIntentBuilder
        {
            builder.Services.AddSingleton<IStoredProcedureNameProvider>(new FixedStoredProcedureNameProvider(name));
            return builder;
        }
        public static TBuilder SetSchema<TBuilder>(this TBuilder builder, string? schema)
           where TBuilder : IStoredProcedureModuleIntentBuilder
        {
            builder.Services.AddSingleton<IStoredProcedureSchemaProvider>(new FixedStoredProcedureSchemaProvider(schema));
            return builder;
        }
    }
}
