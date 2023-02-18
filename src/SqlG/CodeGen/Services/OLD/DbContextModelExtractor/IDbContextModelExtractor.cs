using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SqlG
{
    public interface IDbContextModelExtractor
    {
        IModel Model { get; }
        IReadOnlyCollection<IEntityTypeTable> Entities { get; }
        ISqlTargetOutput DefaultSqlTargetOutput { get; }
        ICsTargetOutput DefaultCsTargetOutput { get; }
        IReadOnlyCollection<ISqlGenActionBuilder> ActionBuilders { get; }

        IReadOnlyCollection<IOperationCommand<CreateTableOperation, MigrationCommand>> CreateTableIntents { get; }
        IReadOnlyCollection<IOperationCommand<CreateIndexOperation, MigrationCommand>> CreateIndexIntents { get; }
        IReadOnlyCollection<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> EnsureSchemaIntents { get; }
        IReadOnlyCollection<IOperationCommand<CreateSequenceOperation, MigrationCommand>> CreateSequenceIntents { get; }

    }
}
