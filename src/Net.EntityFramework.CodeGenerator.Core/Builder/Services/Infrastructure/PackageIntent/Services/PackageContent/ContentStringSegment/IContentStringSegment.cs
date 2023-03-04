using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IContentStringSegment : IPackageContent
    {
        void Build(StringBuilder builder);
    }
}
