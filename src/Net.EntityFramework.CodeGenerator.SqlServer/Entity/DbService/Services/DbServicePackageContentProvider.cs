using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class DbServicePackageContentProvider : IIntentContentProvider
    {
        private readonly IDbServiceCodeGeneratorSource _source;
        public DbServicePackageContentProvider(IDbServiceCodeGeneratorSource source)
        {
            _source = source;
        }

        public IEnumerable<IContent> Get()
        {
            yield return new CommandTextSegment("DbService");
        }
    }


}
