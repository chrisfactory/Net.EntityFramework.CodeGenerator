﻿using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateTablePackageBuilder : PackageBuilder, ICreateTablePackageBuilder
    {
        public CreateTablePackageBuilder(IPackageStack packageStack) : base(packageStack)
        {
        }
        protected override void DefineIntentProviders(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntentProvider<DataProjectTarget, CreateTablePackageContentProvider>();
        }
    }
}
