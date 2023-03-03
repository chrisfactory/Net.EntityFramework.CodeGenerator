using System.Text;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IContentStringSegment : IPackageContent
    {
        void Build(StringBuilder builder);
    }
}
