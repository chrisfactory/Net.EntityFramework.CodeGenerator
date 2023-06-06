using Microsoft.EntityFrameworkCore.Metadata;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageSource
    {
        string? Name { get; }
    }
    public interface IEntityFrameworkSource : IPackageSource
    {
     
    }

    public interface ICreateTableSource : IEntityFrameworkSource
    {
        string? Schema { get; }
    }
    public interface ICreateIndexSource : IEntityFrameworkSource
    {
        string? Schema { get; }
    }

    public interface IEnsureSchemaSource : IEntityFrameworkSource
    {

    }
    public interface ICreateSequenceSource : IEntityFrameworkSource
    {
        
    }
    public interface ICodeGeneratorSource : IPackageSource
    {
    }
    public interface IStoredProcedureCodeGeneratorSource : ICodeGeneratorSource
    {
        string? Schema { get; }

    }


    public interface ISpSelectCodeGeneratorSource : IStoredProcedureCodeGeneratorSource
    {
        Type DbContextType { get; }
        bool IsSelfDbContext { get; }
        string TableName { get; }
        string TableFullName { get; }

        SelectResultSets ResultSet { get; }
        IEntityTypeTable EntityTable { get; }
        IReadOnlyCollection<IEntityColumn> ProjectionColumns { get; }
        IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; }

    }
    public class SelectResultSet : ISelectResultSet
    {
        private SelectResultSet(SelectResultSets resultSet)
        {
            ResultSet = resultSet;
        }
        public SelectResultSets ResultSet { get; }

        public static ISelectResultSet Select() => new SelectResultSet(SelectResultSets.Select);
        public static ISelectResultSet SelectFirst() => new SelectResultSet(SelectResultSets.SelectFirst);
        public static ISelectResultSet SelectFirstOrDefault() => new SelectResultSet(SelectResultSets.SelectFirstOrDefault);
        public static ISelectResultSet SelectSingle() => new SelectResultSet(SelectResultSets.SelectSingle);
        public static ISelectResultSet SelectSingleOrDefault() => new SelectResultSet(SelectResultSets.SelectSingleOrDefault);
    }
    public interface ISelectResultSet
    {
        SelectResultSets ResultSet { get; }
    }
    public enum SelectResultSets
    {
        Select,
        SelectFirst,
        SelectFirstOrDefault,
        SelectSingle,
        SelectSingleOrDefault
    }

    public interface IDbServiceCodeGeneratorSource : ICodeGeneratorSource
    {
        IReadOnlyList<IDotNetContentCodeSegment> Segments { get; }  
    }

    public interface IEntityMapperCodeGeneratorSource : ICodeGeneratorSource
    {
        string? Schema { get; }
        IMutableEntityType Entity { get; }
    }
}
