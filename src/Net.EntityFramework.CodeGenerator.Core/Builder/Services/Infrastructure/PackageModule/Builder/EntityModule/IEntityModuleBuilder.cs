namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IEntityModuleBuilder<TEntity> : IPackageModuleBuilder
         where TEntity : class
    {
    } 
}
