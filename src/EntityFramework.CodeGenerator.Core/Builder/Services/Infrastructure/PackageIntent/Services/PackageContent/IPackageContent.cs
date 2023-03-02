using System.Text;

namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageContent
    {

    }
    public interface IContentFileSegment : IPackageContent
    {
        void Build(StringBuilder builder);
    }
}
