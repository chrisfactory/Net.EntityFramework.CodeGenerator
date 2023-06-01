using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDbContextModelContext
    {
        Type DbContextType { get; }
        bool IsSelfDbContext { get; }

        IModel Model { get; }
        IDotNetProjectTargetInfos DotNetProjectTargetInfos { get; }
        IDataProjectTargetInfos DataProjectTargetInfos { get; }


        IReadOnlyCollection<IEntityTypeTable> Entities { get; }

        IReadOnlyCollection<IOperationCommand<CreateTableOperation, MigrationCommand>> CreateTableIntents { get; }
        IReadOnlyCollection<IOperationCommand<CreateIndexOperation, MigrationCommand>> CreateIndexIntents { get; }
        IReadOnlyCollection<IOperationCommand<EnsureSchemaOperation, MigrationCommand>> EnsureSchemaIntents { get; }
        IReadOnlyCollection<IOperationCommand<CreateSequenceOperation, MigrationCommand>> CreateSequenceIntents { get; }

    }
}
