﻿using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;
using Net.EntityFramework.CodeGenerator.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IEntityModuleBuilderExtensions
    {
        public static IPackageToken EnsureSchemas(this IModelModuleBuilder module)
        {
            return module.UsePackageBuilder<IEnsureSchemaModuleIntentBuilder, EnsureSchemaModuleIntentBuilder>();
        }
    }
}
