using System.Text;

namespace SqlG
{
    internal class CreateFileAction : ISqlGAction
    {
        private readonly IContentFileRootSegment _contentFile;
        private readonly IFileInfosProvider _fileInfosProvider;
        public CreateFileAction(IContentFileRootSegment contentFile, IFileInfosProvider fileInfosProvider)
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
    }
}
