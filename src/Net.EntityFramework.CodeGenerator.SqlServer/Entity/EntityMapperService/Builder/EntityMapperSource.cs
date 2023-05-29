using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EntityMapperSource : IEntityMapperCodeGeneratorSource
    {
        public EntityMapperSource(IPackageToken token, IPackageLink link)
        {
            //var storedProcedures = new List<IStoredProcedureInfos>();
            //var keys = token.CorrelateTokens.ToHashSet();
            //foreach (var package in link.Packages)
            //{
            //    if (keys.Contains(package.Token))
            //    {
            //        foreach (var intent in package.Intents)
            //        {
            //            if (intent.Target is IDbServiceBuilderTarget serviceBuilderTarget)
            //            {
            //                if (intent.Target is IDbServiceSpSelectTarget spSelect)
            //                {
            //                    foreach (var content in intent.Contents)
            //                    {
            //                        if (content is IStoredProcedureInfos storedProcedure)
            //                        {
            //                            storedProcedures.Add(storedProcedure);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }
        public string Name { get; } = "Db Service";
    }
}
