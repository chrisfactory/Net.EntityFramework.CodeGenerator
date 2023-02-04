
using System.Text;

namespace SqlG
{
    public interface IContentFileSegment : IEnumerable<IContentFileSegment>
    {
        void Build(StringBuilder builder);
    }
}
