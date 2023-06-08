using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServiceSource 
    {
        public DbServiceSource(IPackageToken token, IPackageLink link)
        {
            var segments = new List<IDotNetContentCodeSegment>();
            var keys = token.CorrelateTokens.ToHashSet();
            foreach (var package in link.Packages)
            {
                if (keys.Contains(package.Token))
                {
                    foreach (var intent in package.Intents)
                    {
                        if (intent.Target is IDbServiceBuilderTarget serviceBuilderTarget)
                        {
                            foreach (var content in intent.Contents)
                            {
                                if (content is IDotNetContentCodeSegment storedProcedure)
                                {
                                    segments.Add(storedProcedure);
                                }
                            }
                        }
                    }
                }
            }

            Segments = segments;
        }
         
        public string Name { get; } = "Db Service";

        public IReadOnlyList<IDotNetContentCodeSegment> Segments { get; }
    }
}
