namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDataBaseProjectTarget : IPackageTarget
    {

    }

    public interface IStoredProcedureTarget : IDataBaseProjectTarget
    {

    }

   

    public interface IIndexTarget : IDataBaseProjectTarget
    {

    }

    public interface ISequenceTarget : IDataBaseProjectTarget
    {

    }

    public interface IEnsureSchemaTarget : IDataBaseProjectTarget
    {

    }
}
