using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class SequencesPackageContentProvider : IPackageContentProvider
    {
        private readonly IModelPackageScope _scope;
        private readonly IDbContextModelExtractor _model;
        public SequencesPackageContentProvider(IPackageScope scope)
        {
            _scope = (IModelPackageScope)scope;
            _model = _scope.DbContextModel;
        }

        public IEnumerable<IPackageContent> Get()
        {
            foreach (var cmd in _model.CreateSequenceIntents)
                yield return new CommandTextSegment(cmd.Command.CommandText);
        }
    }
}
