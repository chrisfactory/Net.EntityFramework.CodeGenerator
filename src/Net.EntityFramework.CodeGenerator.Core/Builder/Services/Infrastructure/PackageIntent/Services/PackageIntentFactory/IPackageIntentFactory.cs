namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageIntentFactory
    {
        IEnumerable<IPackageIntent> Create();
    }
}
