using System.Text;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleIntentProvider
    {
        IEnumerable<IPackageModuleIntent> Get();
    }
     
}
