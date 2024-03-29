﻿using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EFDbServiceModuleIntentBuilder : PackageBuilder, IEFDbServiceModuleIntentBuilder
    {
        public EFDbServiceModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {

        }
        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DotNetProjectTarget, EFDbServicePackageContentProvider>();
        }
    }
}
