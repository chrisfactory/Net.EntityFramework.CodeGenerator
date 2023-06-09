using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator
{
    public interface ISpSelectPackageBuilder<TEntity> : IStoredProcedurePackageBuilder
         where TEntity : class
    {
    }
}
