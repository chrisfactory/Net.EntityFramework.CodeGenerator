using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IContentStringSegment : IContent
    {
        void Build(StringBuilder builder);
    } 
}
