﻿using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EntityMapperModuleIntentBuilder : PackageBuilder, IEntityMapperModuleIntentBuilder
    {
        public EntityMapperModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {

        }
        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DotNetProjectTarget, EntityMapperPackageContentProvider>();

        }
    }
}
