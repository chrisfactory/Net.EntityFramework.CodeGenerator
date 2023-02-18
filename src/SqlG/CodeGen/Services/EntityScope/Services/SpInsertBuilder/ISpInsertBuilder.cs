using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public interface ISpInsertBuilder : ISqlGenActionBuilder
    {
    }

    internal class SpInsertBuilder : SqlGenActionBuilder, ISpInsertBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            throw new NotImplementedException();
        }
    }
}
