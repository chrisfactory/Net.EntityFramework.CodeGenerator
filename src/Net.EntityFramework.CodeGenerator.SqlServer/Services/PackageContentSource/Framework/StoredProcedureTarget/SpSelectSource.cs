using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SpSelectSource : ISpSelectCodeGeneratorSource
    {
        public SpSelectSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Sp Select";
        public IPackageScope Scope { get; }
    }
}
