using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSource : IDbServiceCodeGeneratorSource
    {
        public DbServiceSource(IPackageLink link, ITokenLink tokenLink)
        {
            var keys = tokenLink.Tokens.ToHashSet();
            foreach (var package in link.Packages)
            {
                if(keys.Contains(package.Token))
                {
                    foreach (var intent in package.Intents)
                    {
                        if (intent.Target is IDbServiceBuilderTarget serviceBuilderTarget)
                        {
                            if (intent.Target is IDbServiceSpSelectTarget spSelect)
                            {

                            }
                        }
                    }
                }
            }
        }
        public string Name { get; } = "Db Service";
    }
}
