using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IContentStoredProcedureInfos : IPackageContent
    {
        string StoredProcedureName { get; }
       
    }
}
