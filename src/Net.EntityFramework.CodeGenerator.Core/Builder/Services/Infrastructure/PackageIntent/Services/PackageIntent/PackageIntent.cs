namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class PackageIntent : IPackageIntent
    {
        public PackageIntent(IPackageTarget target, IPackageContent content)
        {
            Target = target;
            Content = content;
        } 

        public IPackageTarget Target { get; }

        public IPackageContent Content { get; }

    }
}
