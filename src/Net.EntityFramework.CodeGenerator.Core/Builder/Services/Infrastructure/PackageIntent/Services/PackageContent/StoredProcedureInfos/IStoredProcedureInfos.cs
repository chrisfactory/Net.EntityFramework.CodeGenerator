namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IStoredProcedureInfos : IContent
    {
        string StoredProcedureName { get; }  
        IReadOnlyCollection<IEntityColumn> Parameters { get; }
    }
}
