using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IModulePackage
    {
        IEnumerable<IPackage> Packages { get; }

    }
    internal class Module : IModulePackage
    {
        public Module(
            IPackageTokenProvider tokenProvider,
            IEnumerable<IPackageBuilderKey> packagesBuilders)
        {

            foreach (var builder in packagesBuilders)
            {
                if (!tokenProvider.Checked(builder.Token))
                {
                    throw new InvalidOperationException();
                }
            }




            Packages = Build(packagesBuilders);
       
        }
        public IEnumerable<IPackage> Packages { get; }



        private IEnumerable<IPackage> Build(IEnumerable<IPackageBuilderKey> packagesBuilders)
        {

            var builderStore = packagesBuilders.ToDictionary(b => b.Token, b => new BuilderItem(b));

            foreach (var item in builderStore)
                item.Value.UseStorage(builderStore);

            foreach (var item in builderStore)
                item.Value.SetLevel();

            foreach (var levelGroup in builderStore.Values.GroupBy(box => box.Level).OrderBy(g => g.Key))
            {
                foreach (var builder in levelGroup)
                {
                    builder.Build();
                }
            } 

            return builderStore.Values.Select(box=>box.GetResult()).ToList();
        }



        private class BuilderItem
        {
            private Dictionary<IPackageToken, BuilderItem> dependencies = new Dictionary<IPackageToken, BuilderItem>();
            private List<BuilderItem> _childrens = new List<BuilderItem>(); 
            private int _maxLevel = 0;
            private IPackage? _BuildResult = null;
            internal BuilderItem(IPackageBuilderKey builder)
            {
                Builder = builder;

            }
            internal IPackageBuilderKey Builder { get; }
            internal int Level { get; private set; }
            internal bool IsRoot { get; private set; }
           
            internal void UseStorage(IReadOnlyDictionary<IPackageToken, BuilderItem> commonStorage)
            {
                _maxLevel = commonStorage.Count - 1;
                foreach (var correlationToken in Builder.Token.CorrelateTokens)
                {
                    var builder = commonStorage[correlationToken];
                    builder.AddChild(this);
                    dependencies.Add(correlationToken, builder);
                }
                IsRoot = dependencies.Count == 0;
            }

            private void AddChild(BuilderItem item)
            {
                _childrens.Add(item);
            }

            internal void SetLevel()
            {

                if (IsRoot)
                    foreach (var child in _childrens)
                        child.SetLevelToChildrens(1);
                else if (this.Level == 0)
                    throw new InvalidOperationException("circular");
            }

            private void SetLevelToChildrens(int nextLevel)
            {
                if (nextLevel > _maxLevel)
                    throw new InvalidOperationException("circular");

                if (this.Level < nextLevel)
                    this.Level = nextLevel;
                foreach (var child in _childrens)
                    child.SetLevelToChildrens(this.Level + 1);
            }

            internal void Build()
            {
                lock (this)
                {
                    if(_BuildResult != null )
                        throw new InvalidOperationException();

                    var relatedPackages = new List<IPackage>();
                    foreach (var item in dependencies.Values)
                    {
                        var result = item._BuildResult;
                        if (result != null)
                            relatedPackages.Add(result);
                        else
                            throw new InvalidOperationException();
                    } 

                    var builder = Builder.Builder;
                    builder.Services.AddSingleton<IPackageLink>(p => new PackageLync(relatedPackages));
                    _BuildResult = Builder.Builder.Build();
                }
              
            }

           
            internal IPackage GetResult()
            {
                if(_BuildResult == null )
                    throw new InvalidOperationException();
                return _BuildResult;
            }


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
}