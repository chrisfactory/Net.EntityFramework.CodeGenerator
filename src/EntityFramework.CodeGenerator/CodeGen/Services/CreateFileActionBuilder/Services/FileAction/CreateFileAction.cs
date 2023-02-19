using System.Text;

namespace EntityFramework.CodeGenerator
{
    internal class CreateFileAction : ISqlGenAction
    {
        private readonly IContentFileSegment _contentFile;
        private readonly IFileInfosProvider _fileInfosProvider;
        public CreateFileAction(IContentFileSegment contentFile, IFileInfosProvider fileInfosProvider)
        {
            _contentFile = contentFile;
            _fileInfosProvider = fileInfosProvider;
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            var sb = new StringBuilder();
            _contentFile.Build(sb);
            var content = sb.ToString();
            foreach (var targetPath in _fileInfosProvider.Get())
            {
                targetPath.Directory?.Create();
                await File.WriteAllTextAsync(targetPath.FullName, content, token);
            }
        }

        public override string? ToString()
        {
            string? str = null;
            foreach (var fi in _fileInfosProvider.Get())
            {
                str += $"[{fi.FullName}]";
            }
            return string.IsNullOrWhiteSpace(str) ? base.ToString() : str;
        }
    }
}
