using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEFDbServiceModuleIntentBuilderExtensions
    {
        public static IPackageToken EFDbService<TEntity>(this IEntityModuleBuilder<TEntity> module)
             where TEntity : class
        {
            return module.UsePackageBuilder<IEFDbServiceModuleIntentBuilder, EFDbServiceModuleIntentBuilder>();
        }
    }
}
