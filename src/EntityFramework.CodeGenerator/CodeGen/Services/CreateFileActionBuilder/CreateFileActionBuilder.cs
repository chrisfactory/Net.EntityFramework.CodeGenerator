using EntityFramework.CodeGenerator.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace EntityFramework.CodeGenerator
{
    public interface IPackageContent
    {

    }
    public interface IContentFileSegment : IPackageContent
    {
        void Build(StringBuilder builder);
    }


    internal class CreateFileActionBuilder : ICreateFileActionBuilder, IActionBuilder
    {
        public CreateFileActionBuilder()
        {
            RequiredDependencies = new List<IRequiredDependencyService>();
            Services = new ServiceCollection();
        }

        public IServiceCollection Services { get; }

        public IReadOnlyCollection<IRequiredDependencyService> RequiredDependencies { get; }


        IActionProvider IActionBuilder.Build()
        {
            Services.AddSingleton<ICreateFileActionFactory, CreateFileActionFactory>();
            Services.AddSingleton<IActionProvider, CreateFileActionProvider>();
            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IActionProvider>();
        }
    }

    public interface ICreateFileActionFactory
    {
        IEnumerable<IAction> Create();
    }
    internal class CreateFileActionFactory : ICreateFileActionFactory
    {
        private readonly IFileInfosProvider _fiProvider; 
        private readonly IContentFileSegment _content;
        public CreateFileActionFactory(IFileInfosProvider fiProvider, IContentFileSegment content)
        {
            _fiProvider = fiProvider;
            _content = content;
        }
        public IEnumerable<IAction> Create()
        {
            foreach (var fi in _fiProvider.Get())
            {
                yield return new CreateFileAction(_content, fi);
            }
        }
    }

    internal class CreateFileActionProvider : IActionProvider
    {
        private readonly ICreateFileActionFactory _createFileFactory;
        public CreateFileActionProvider(ICreateFileActionFactory createFileFactory)
        {
            _createFileFactory = createFileFactory; 
        }
        public IEnumerable<IAction> Get()
        {
            return _createFileFactory.Create();
        }
    }


    public interface ICreateFileAction : IAction
    {
        IContentFileSegment Segment { get; }
        FileInfo FileInfo { get; }
    }
    internal class CreateFileAction : ICreateFileAction
    {
        public CreateFileAction(IContentFileSegment contentFile, FileInfo fi)
        {
            Segment = contentFile;
            FileInfo = fi;
        }
        public IContentFileSegment Segment { get; }
        public FileInfo FileInfo { get; }
        public async Task ExecuteAsync(CancellationToken token)
        {
            var sb = new StringBuilder();
            Segment.Build(sb);
            var content = sb.ToString();

            FileInfo.Directory?.Create();
            await File.WriteAllTextAsync(FileInfo.FullName, content, token);
        }

        public override string? ToString()
        {
            return $"[{FileInfo.FullName}]";
        }
    }
}
