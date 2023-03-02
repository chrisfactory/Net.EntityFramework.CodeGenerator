using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator.Core
{
    internal class CreateTableModuleIntentBuilder : ICreateTableModuleIntentBuilder
    {
        public CreateTableModuleIntentBuilder(IMutableEntityType metadata, IDbContextModelExtractor model)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(metadata);
            Services.AddSingleton(model);
            Services.AddSingleton<IPackageScope, TablePackageScope>();
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<TablePackageScope, CreateTableSource, TableTarget, CreateTablePackageContentProvider>>();
        }

        public IServiceCollection Services { get; }

        public IPackageModuleIntent Build()
        {
            Services.AddSingleton<IPackageIntentFactory, PackageIntentFactory>();
            Services.AddSingleton(p => p.GetRequiredService<IPackageIntentFactory>().Create());
            Services.AddSingleton<IPackageModuleIntent, PackageModuleIntent>();
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
