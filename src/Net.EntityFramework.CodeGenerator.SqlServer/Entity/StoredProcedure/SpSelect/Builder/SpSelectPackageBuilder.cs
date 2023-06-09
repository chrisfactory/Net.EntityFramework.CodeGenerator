using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{

    public class StoredProcedureEfCallerNameProvider : IStoredProcedureEfCallerNameProvider
    {
        private readonly IMutableEntityType _mutableEntity;
        private readonly ISpSelectParametersProvider _parametetersProvider;
        public StoredProcedureEfCallerNameProvider(
            IMutableEntityType mutableEntity,
            ISpSelectParametersProvider parametersProvider)
        {
            _mutableEntity = mutableEntity;
            _parametetersProvider = parametersProvider;
        }
        public string Get()
        {
            var name = _mutableEntity.ClrType.Name;

            string by = string.Empty;
            string all = string.Empty;

            var parameters = _parametetersProvider.GetParameters();
            if (parameters == null || parameters.Count == 0)
                all = "All";
            else
            {
                by += "By";
                foreach (var parameter in parameters)
                    by += parameter.PropertyName;
            }
            return $"Get{all}{name}{by}";
        }
    }

    public class SpSelectPackageBuilder<TEntity> : PackageBuilder, ISpSelectPackageBuilder<TEntity>
         where TEntity : class
    {
        public SpSelectPackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<ISpSelectParametersProvider, SpSelectPrimaryKeysParameters>();
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SpSelectNameProvider>();
            Services.AddSingleton<IStoredProcedureEfCallerNameProvider, StoredProcedureEfCallerNameProvider>();
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, SqlSpSelectPackageContentProvider>();
            intentBuilder.DefineIntentProvider<DbServiceBuilderTarget, DbServiceSpSelectPackageContentProvider>();
            intentBuilder.DefineIntentProvider<EfDbContextExtensionBuilderTarget, EfDbContextExtensionSpSelectPackageContentProvider>();
        }
    }
}
