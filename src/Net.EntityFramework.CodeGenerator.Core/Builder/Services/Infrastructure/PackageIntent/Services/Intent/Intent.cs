namespace Net.EntityFramework.CodeGenerator.Core
{
    public class Intent : IIntent
    {
        public Intent(ITarget target, IEnumerable<IContent> contents)
        {
            Target = target;
            Contents = contents;
        }  
        public ITarget Target { get; } 
        public IEnumerable<IContent> Contents { get; }
    }
}
