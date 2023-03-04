using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesSource : ICreateSequenceSource
    {
        public SequencesSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Create Sequences";
        public IPackageScope Scope { get; }
    }
}
