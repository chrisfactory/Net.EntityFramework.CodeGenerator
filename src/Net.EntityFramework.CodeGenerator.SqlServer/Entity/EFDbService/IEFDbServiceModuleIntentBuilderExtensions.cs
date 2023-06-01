using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEFDbServiceModuleIntentBuilderExtensions
    {
        public static IPackageToken EFDbService(this IEntityModuleBuilder module)
        {
            return module.UsePackageBuilder<IEFDbServiceModuleIntentBuilder, EFDbServiceModuleIntentBuilder>();
        }
    }
}
