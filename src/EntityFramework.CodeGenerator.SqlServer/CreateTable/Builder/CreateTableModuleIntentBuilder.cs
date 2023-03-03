using EntityFramework.CodeGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTableModuleIntentBuilder : ICreateTableModuleIntentBuilder
    {
        public CreateTableModuleIntentBuilder(IServiceCollection stack)
        {
            Services = stack;
            Services.AddSingleton<IPackageScope, TablePackageScope>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<CreateTableSource, TableTarget, CreateTablePackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }
    }

    internal class CreateTablePackageContentProvider : IPackageContentProvider
    {
        private readonly ITablePackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public CreateTablePackageContentProvider(IPackageScope scope, IDbContextModelExtractor model)
        {
            _scope = (ITablePackageScope)scope;
            _model = model;
        }
        public IPackageContent Get()
        {
            var tableName = _scope.MetaData.GetTableName();
            var cmd = _model.CreateTableIntents.Single(o => o.Operation.Name == tableName);
            return new CommandTextSegment(cmd.Command.CommandText);
        }
    }
}
