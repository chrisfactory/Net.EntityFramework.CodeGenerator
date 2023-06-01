using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EntityMapperModuleIntentBuilder : PackageBuilder<IEntityMapperCodeGeneratorSource, EntityMapperSource>, IEntityMapperModuleIntentBuilder
    {
        public EntityMapperModuleIntentBuilder(IPackageStack packageStack) : base(packageStack)
        {

        }
        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<DotNetProjectTarget, EntityMapperPackageContentProvider>();

        }
    }
}
