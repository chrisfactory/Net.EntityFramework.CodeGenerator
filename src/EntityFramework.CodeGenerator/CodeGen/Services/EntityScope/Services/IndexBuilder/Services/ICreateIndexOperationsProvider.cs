using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EntityFramework.CodeGenerator
{
    public interface ICreateIndexOperationsProvider
    {
        IReadOnlyCollection<IOperationCommand<CreateIndexOperation, MigrationCommand>> Get();
    }
    internal class CreateIndexOperationsProvider : ICreateIndexOperationsProvider
    {
        private readonly IReadOnlyCollection<IOperationCommand<CreateIndexOperation, MigrationCommand>> _commands;
        public CreateIndexOperationsProvider(IDbContextModelExtractor modelExtractor, IEntityTypeTable entity)
        {
            _commands = modelExtractor.CreateIndexIntents.Where(c => c.GetTableFullName() == entity.TableFullName).ToList();
        }
        public IReadOnlyCollection<IOperationCommand<CreateIndexOperation, MigrationCommand>> Get()
        {
            return _commands;
        }
    }
}
