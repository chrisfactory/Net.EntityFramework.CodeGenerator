using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesSource : ICreateSequenceSource
    { 
        public string Name { get; } = "Create Sequences";
    }
}
