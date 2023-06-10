namespace Net.EntityFramework.CodeGenerator.Core
{
    public class ResultSetProvider : IResultSet
    {
        private ResultSetProvider(ResultSets resultSet)
        {
            ResultSet = resultSet;
        }
        public ResultSets ResultSet { get; }

        public static IResultSet Default() => new ResultSetProvider(ResultSets.None);
        public static IResultSet First() => new ResultSetProvider(ResultSets.First);
        public static IResultSet FirstOrDefault() => new ResultSetProvider(ResultSets.FirstOrDefault);
        public static IResultSet Single() => new ResultSetProvider(ResultSets.Single);
        public static IResultSet SingleOrDefault() => new ResultSetProvider(ResultSets.SingleOrDefault);
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
