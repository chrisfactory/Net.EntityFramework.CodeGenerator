using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace EntityFramework.CodeGenerator
{
    public interface IEntityTypeTable
    {
        IModel Model { get; }
        IEntityType EntityType { get; }
        ITable Table { get; }
        string TableFullName { get; }

        IReadOnlyCollection<ISqlGenActionBuilder> ActionBuilders { get; }

        IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; }
        IReadOnlyCollection<IEntityColumn> InsertColumns { get; }
        IReadOnlyCollection<IEntityColumn> UpdatableColumns { get; }
        IReadOnlyCollection<IEntityColumn> AllColumns { get; }
    }
    public interface IEntityColumn
    {
        bool IsPrimaryKey { get; }

        string ColumnName { get; }
        string SqlType { get; }

        PropertyInfo? PropertyInfo { get; }
        string? PropertyName { get; }
        string PropertyType { get; }

        IColumn Column { get; }
    }

}
