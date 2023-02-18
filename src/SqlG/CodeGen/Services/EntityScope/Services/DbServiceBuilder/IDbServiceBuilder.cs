namespace SqlG
{
    public interface IDbServiceBuilder : ISqlGenActionBuilder
    {
    }

    internal class DbServiceBuilder : SqlGenActionBuilder, IDbServiceBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            throw new NotImplementedException();
        }
    }
}
