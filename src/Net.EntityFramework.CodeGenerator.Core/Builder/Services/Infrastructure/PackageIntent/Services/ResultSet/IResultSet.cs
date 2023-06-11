namespace Net.EntityFramework.CodeGenerator.Core
{
    public class ResultSetProvider : IResultSet
    {
        private ResultSetProvider(ResultSets resultSet)
        {
            ResultSet = resultSet;
        }
        public ResultSets ResultSet { get; }

        public static IResultSet None() => new ResultSetProvider(ResultSets.None);
        public static IResultSet Enumerable() => new ResultSetProvider(ResultSets.Enumerable);
        public static IResultSet List() => new ResultSetProvider(ResultSets.List);
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
        Enumerable,
        List,
        First,
        FirstOrDefault,
        Single,
        SingleOrDefault
    }


}
