using System.Diagnostics;

namespace Net.EntityFramework.CodeGenerator.Core
{
    [DebuggerDisplay("{FileInfo}")]
    public class ContentFile : IContentFile
    {
        public ContentFile(FileInfo fileInfo, IContentStringSegment content)
        {
            FileInfo = fileInfo;
            ContentBuilder = content;
        }

        public FileInfo FileInfo { get; }

        public IContentStringSegment ContentBuilder { get; }

    }
}
