using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IContentFile : IContent
    {
        FileInfo FileInfo { get; }
        IContentStringSegment ContentBuilder { get; }
    }
}
