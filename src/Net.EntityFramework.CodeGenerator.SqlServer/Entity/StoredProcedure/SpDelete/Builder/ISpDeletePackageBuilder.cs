using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator
{
    public interface ISpDeletePackageBuilder<TEntity> : IStoredProcedurePackageBuilder
         where TEntity : class
    {
    }
}
