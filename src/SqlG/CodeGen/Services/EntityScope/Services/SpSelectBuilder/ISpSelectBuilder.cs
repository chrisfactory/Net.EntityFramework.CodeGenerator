using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public interface ISpSelectBuilder : ISqlGenActionBuilder
    {

    }

    internal class SpSelectBuilder : SqlGenActionBuilder, ISpSelectBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            throw new NotImplementedException();
        }
    }
}
