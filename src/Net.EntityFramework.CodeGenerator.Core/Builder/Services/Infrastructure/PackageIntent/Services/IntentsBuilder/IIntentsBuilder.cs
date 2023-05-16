using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IIntentsBuilder : IBuilder<IEnumerable<IIntent>>
    {
        INodeSnapshotPoint SnapshotPoint { get; }
    }
}
