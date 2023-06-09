using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IEntityColumn
    {
        bool IsPrimaryKey { get; }

        string ColumnName { get; }
        string SqlType { get; }

        PropertyInfo? PropertyInfo { get; }
        string? PropertyName { get; }
        string? CallPropertyName { get; }
        string PropertyType { get; }

        IColumn Column { get; }
    }
}
