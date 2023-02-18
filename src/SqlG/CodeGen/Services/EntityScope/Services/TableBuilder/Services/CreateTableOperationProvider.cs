using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SqlG
{
    public interface ICreateTableOperationProvider
    {
        IOperationCommand<CreateTableOperation, MigrationCommand> Get();
    }
    internal class CreateTableOperationProvider : ICreateTableOperationProvider
    {
        private readonly IOperationCommand<CreateTableOperation, MigrationCommand> _command;
        public CreateTableOperationProvider(IDbContextModelExtractor modelExtractor, IEntityTypeTable entity)
        {
            _command = modelExtractor.CreateTableIntents.Single(c => c.GetTableFullName() == entity.TableFullName);
        }
        public IOperationCommand<CreateTableOperation, MigrationCommand> Get()
        {
            return _command;
        }
    }
}
