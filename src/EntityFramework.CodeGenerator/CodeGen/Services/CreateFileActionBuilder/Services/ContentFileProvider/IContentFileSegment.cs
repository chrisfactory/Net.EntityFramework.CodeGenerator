
using System.Text;

namespace EntityFramework.CodeGenerator
{
    public interface IContentFileSegment  
    {
        void Build(StringBuilder builder);
    }
}
