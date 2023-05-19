namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageTokenProvider
    {
        IPackageToken CreateToken();
        bool Checked(IPackageToken token);
    }
}
