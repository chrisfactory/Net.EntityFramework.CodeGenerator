namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageContentSource
    {
        string Name { get; }
        IPackageScope Scope { get; }
    }

    public interface IEntityFrameworkSource : IPackageContentSource
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
    public interface ICodeGeneratorSource : IPackageContentSource
    {
    }
    public interface IStoredProcedureCodeGeneratorSource : ICodeGeneratorSource
    {
    }


    public interface ISpSelectCodeGeneratorSource : IStoredProcedureCodeGeneratorSource
    {

    }
}
