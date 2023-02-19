namespace EntityFramework.CodeGenerator
{
    internal class TargetFiles : IFileInfosProvider
    {
        private readonly FileInfo[] _target;
        public TargetFiles(FileInfo[] target)
        {
            _target = target;
        }
        public FileInfo[] Get()
        {
            return _target;
        }
    }
}
