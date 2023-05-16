using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IModulePackage
    {
        IEnumerable<IPackage> Packages { get; }

    }
    internal class Module : IModulePackage
    {
        public Module(IEnumerable<IPackage> packages, IEnumerable<IPackageBuilder> builders)
        {
            foreach (IPackageBuilder builder in builders)
                builder.Services.AddSingleton<IPackageLink>(new PackageLync(packages));

            var finalPackages = packages.ToList();
            foreach (IPackageBuilder builder in builders)
                finalPackages.Add(builder.Build());

            Packages = finalPackages;
        }
        public IEnumerable<IPackage> Packages { get; }


        private class PackageLync : IPackageLink
        {
            public PackageLync(IEnumerable<IPackage> packages)
            {
                Packages = packages;
            }
            public IEnumerable<IPackage> Packages { get; }
        }
    }
}