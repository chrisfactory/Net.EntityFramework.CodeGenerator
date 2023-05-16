using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesTarget : Target, ISequenceTarget
    {
        public SequencesTarget(IPackageToken token) : base(token)
        {
        }
    }
}
