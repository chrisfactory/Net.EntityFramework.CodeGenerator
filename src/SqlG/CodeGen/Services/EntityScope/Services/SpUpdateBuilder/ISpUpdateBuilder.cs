namespace SqlG
{
    public interface ISpUpdateBuilder : ISqlGenActionBuilder
    {
    }

    internal class SpUpdateBuilder : SqlGenActionBuilder, ISpUpdateBuilder
    {

        public override ISqlGenActionProvider Build()
        {
            throw new NotImplementedException();
        }
    }
}
