using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IEntityTypeTable
    {
        IModel Model { get; }
        IEntityType EntityType { get; }
        ITable Table { get; }
        string TableFullName { get; }


        IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; }
        IReadOnlyCollection<IEntityColumn> InsertColumns { get; }
        IReadOnlyCollection<IEntityColumn> UpdatableColumns { get; }
        IReadOnlyCollection<IEntityColumn> AllColumns { get; }
    }
}
