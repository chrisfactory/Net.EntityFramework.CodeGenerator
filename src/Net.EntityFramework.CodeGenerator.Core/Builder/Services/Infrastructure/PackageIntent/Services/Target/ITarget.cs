namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface ITarget
    {
        IPackageToken Token { get; }
    }
    public abstract class Target
    {
        public Target(IPackageToken token)
        {
            Token = token;
        }
        public IPackageToken Token { get; }
    }
}
