using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EnsureSchemaModuleIntentBuilder : IEnsureSchemaModuleIntentBuilder
    {
        public EnsureSchemaModuleIntentBuilder(IPackageStack packageStack)
        {
            Services = packageStack.GetNewStack<IEnsureSchemaSource, EnsureSchemaSource>();


        }
        //protected override void Prepare(IPackageIntentBuilderFactory preBuilder)
        //{
        //    preBuilder.DefineIntentBuilder<EnsureSchemaTarget, EnsureSchemaPackageContentProvider>();
        //}
        public IServiceCollection Services { get; }

        public IPackage Build()
        {
            return Services.BuildServiceProvider().GetRequiredService<IPackage>();
        }
    }
}
