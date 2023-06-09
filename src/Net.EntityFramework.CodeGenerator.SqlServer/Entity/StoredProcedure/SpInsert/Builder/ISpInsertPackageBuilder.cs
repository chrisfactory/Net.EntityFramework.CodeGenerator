using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator
{
    public interface ISpInsertPackageBuilder<TEntity> : IStoredProcedurePackageBuilder
         where TEntity : class
    {
    }
}
