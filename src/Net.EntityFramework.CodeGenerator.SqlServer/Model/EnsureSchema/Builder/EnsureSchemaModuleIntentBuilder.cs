﻿using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaModuleIntentBuilder : PackageBuilder, IEnsureSchemaModuleIntentBuilder
    {
        public EnsureSchemaModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }

        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, EnsureSchemaPackageContentProvider>();
        }
    }
}
