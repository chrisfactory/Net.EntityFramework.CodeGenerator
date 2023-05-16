namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IIntent
    {
        ITarget Target { get; }
        IEnumerable<IContent> Contents { get; } 
    } 
}
