using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken CreateIndex(this IEntityModuleBuilder module)
        {
            return module.UsePackageBuilder<ICreateIndexPackageBuilder, CreateIndexPackageBuilder>();
        }
    }
}
