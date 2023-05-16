namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageSource
    {
        string Name { get; }
    }
    public interface IEntityFrameworkSource : IPackageSource
    {
    }

    public interface ICreateTableSource : IEntityFrameworkSource
    {

    }
    public interface ICreateIndexSource : IEntityFrameworkSource
    {

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
        string TableName { get; }
        string TableFullName { get; }
        IEntityTypeTable EntityTable { get; }
        IReadOnlyCollection<IEntityColumn> ProjectionColumns { get; }
        IReadOnlyCollection<IEntityColumn> PrimaryKeys { get; }

    }


    public interface IDbServiceCodeGeneratorSource : ICodeGeneratorSource
    {
    }
}
