using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleIntentProvider
    {
        IEnumerable<IPackageModuleIntent> Get();
    }
     
}
