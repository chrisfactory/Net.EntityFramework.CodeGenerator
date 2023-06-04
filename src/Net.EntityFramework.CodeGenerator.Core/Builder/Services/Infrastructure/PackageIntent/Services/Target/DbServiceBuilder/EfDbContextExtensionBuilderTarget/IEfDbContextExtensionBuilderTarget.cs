namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IEfDbContextExtensionBuilderTarget : ITarget
    {

    }

    public class EfDbContextExtensionBuilderTarget : IDbServiceBuilderTarget
    {
        public EfDbContextExtensionBuilderTarget(IPackageToken token)
        {
            Token = token;
        }

        public IPackageToken Token { get; }
    }
}
