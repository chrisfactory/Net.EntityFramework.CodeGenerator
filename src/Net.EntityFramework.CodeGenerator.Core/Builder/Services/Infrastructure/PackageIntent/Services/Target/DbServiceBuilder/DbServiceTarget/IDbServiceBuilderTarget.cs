namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDbServiceBuilderTarget : ITarget
    {

    }

    public class DbServiceBuilderTarget : IDbServiceBuilderTarget
    {
        public DbServiceBuilderTarget(IPackageToken token)
        {
            Token = token;
        }

        public IPackageToken Token { get; }
    }
}
