using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net.EntityFramework.CodeGenerator;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken CreateTable(this IEntityModuleBuilder module)
        { 
            return module.UsePackageBuilder<ICreateTablePackageBuilder, CreateTablePackageBuilder>();
        }
    }
}
