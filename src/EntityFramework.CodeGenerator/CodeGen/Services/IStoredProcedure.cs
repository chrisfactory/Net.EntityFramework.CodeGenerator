namespace EntityFramework.CodeGenerator
{
    public interface IStoredProcedure : ISqlGenActionProvider
    {
        public string Name { get; }
        public IReadOnlyCollection<ICodeAction> SqlCodeActions { get; }
        public IReadOnlyCollection<ICodeAction> NetCodeActions { get; }
    }

    public interface ICodeAction
    {
        ICodeTargetAction Target { get; }
        IContentFileSegment Segment { get; }
    }
    public interface ICodeTargetAction
    {

    }
}
