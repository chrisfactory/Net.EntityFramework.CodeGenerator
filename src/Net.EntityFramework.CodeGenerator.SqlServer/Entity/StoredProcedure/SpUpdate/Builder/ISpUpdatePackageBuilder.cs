using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator
{
    public interface ISpUpdatePackageBuilder<TEntity> : IStoredProcedurePackageBuilder
         where TEntity : class
    {
    }
}
