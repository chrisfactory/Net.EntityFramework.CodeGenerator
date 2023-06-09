namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDataProjectTarget : ITarget
    { 
    }

    public class DataProjectTarget : IDataProjectTarget
    {
        public DataProjectTarget(IPackageToken token)
        {
            Token = token;
        }
        public IPackageToken Token { get; }
    }
}
