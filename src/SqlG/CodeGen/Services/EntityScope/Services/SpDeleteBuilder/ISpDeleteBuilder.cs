using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public interface ISpDeleteBuilder : ISqlGenActionBuilder
    {
    }

    internal class SpDeleteBuilder : SqlGenActionBuilder, ISpDeleteBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            throw new NotImplementedException();
        }

    }
}
