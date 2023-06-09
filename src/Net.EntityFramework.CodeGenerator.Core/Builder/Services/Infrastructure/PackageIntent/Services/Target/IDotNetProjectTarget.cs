namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDotNetProjectTarget : ITarget
    {

    }

    public class DotNetProjectTarget : IDotNetProjectTarget
    {
        public DotNetProjectTarget(IPackageToken token)
        {
            Token = token;
        }
        public IPackageToken Token { get; }
    }
}
