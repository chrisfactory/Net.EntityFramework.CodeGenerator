namespace Net.EntityFramework.CodeGenerator.Core
{
    public class ResultSetProvider : IResultSet
    {
        private ResultSetProvider(ResultSets resultSet)
        {
            ResultSet = resultSet;
        }
        public ResultSets ResultSet { get; }

        public static IResultSet Select() => new ResultSetProvider(ResultSets.None);
        public static IResultSet SelectFirst() => new ResultSetProvider(ResultSets.First);
        public static IResultSet SelectFirstOrDefault() => new ResultSetProvider(ResultSets.FirstOrDefault);
        public static IResultSet SelectSingle() => new ResultSetProvider(ResultSets.Single);
        public static IResultSet SelectSingleOrDefault() => new ResultSetProvider(ResultSets.SingleOrDefault);
    }
    public interface IResultSet
    {
        ResultSets ResultSet { get; }
    }
    public enum ResultSets
    {
        None,
        First,
        FirstOrDefault,
        Single,
        SingleOrDefault
    }


}
