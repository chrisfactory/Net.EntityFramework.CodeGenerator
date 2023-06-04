namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDotNetContentCodeSegment : IContent
    {
        List<string> Usings { get; }
        void Build(ICodeBuilder builder);
    }
}
