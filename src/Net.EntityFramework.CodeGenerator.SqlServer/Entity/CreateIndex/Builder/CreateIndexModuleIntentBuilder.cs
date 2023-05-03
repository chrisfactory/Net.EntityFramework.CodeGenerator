using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class CreateIndexModuleIntentBuilder : PackageModuleIntentBuilder<ICreateIndexSource, CreateIndexSource>, ICreateIndexModuleIntentBuilder
    {
        public CreateIndexModuleIntentBuilder(IModuleStack moduleStack) : base(moduleStack)
        {
            Services.AddSingleton<IPackageIntentBuilder, PackageIntentBuilder<ICreateIndexSource, IndexTarget, CreateIndexPackageContentProvider>>();
        }

        public override IPackageModuleIntent Build()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IPackageModuleIntent>();
        }


    }
}
