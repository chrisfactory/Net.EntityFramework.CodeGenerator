namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IFileGeneratorTarget : ITarget
    { 
    }

    public class FileGeneratorTarget : IFileGeneratorTarget
    {
        public FileGeneratorTarget(IPackageToken token)
        {
            Token = token;
        }
        public IPackageToken Token { get; }
    }
}
