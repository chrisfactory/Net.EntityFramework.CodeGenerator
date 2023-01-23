using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace DataBaseAccess
{
    public class DBAccessBase
    {
        private readonly string _cnx;
        public DBAccessBase(string cnx)
        {
            _cnx = cnx;
        }
        protected async Task<SqlDataReader> ExecuteReaderAsync(string storedProcedureName, IReadOnlyDictionary<string, object> parameters, int? timeout = null)
        {
            var conn = new SqlConnection(_cnx);
            await conn.OpenAsync();

            await using (var cmd = new SqlCommand())
            {

                if (parameters != null)
                    foreach (string key in parameters.Keys)
                    {
                        SqlParameter parameter = new SqlParameter(key, parameters[key]);

                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;

                        if (parameter.SqlDbType == SqlDbType.DateTime && parameter.Value is DateTime && (DateTime)parameter.Value == default(DateTime))
                            parameter.Value = SqlDateTime.MinValue.Value;

                        cmd.Parameters.Add(parameter);

                    }

                cmd.Connection = conn;
                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                //conn.Open();

                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }


















        //if (storedProcedureName == null)
        //    throw new ArgumentNullException("storedProcedureName");


        //parameters = parameters ?? new Dictionary<string, object>();
        ///// Check for SQL Paramter Logging

        //// 1.  create a command object identifying the stored procedure
        //using (SqlCommand cmd = new SqlCommand(storedProcedureName))
        //{   

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    if (timeout != null)
        //        cmd.CommandTimeout = timeout.Value;

        //    foreach (string key in parameters.Keys)
        //    {
        //        SqlParameter parameter = new SqlParameter(key, parameters[key]);

        //        if (parameter.Value == null)
        //            parameter.Value = DBNull.Value;

        //        if (parameter.SqlDbType == SqlDbType.DateTime && parameter.Value is DateTime && (DateTime)parameter.Value == default(DateTime))
        //            parameter.Value = SqlDateTime.MinValue.Value;

        //        cmd.Parameters.Add(parameter);

        //    }


        //    SqlDatabase database = new SqlDatabase(crud.ConnectionString);

        //    if (typeof(T) == typeof(IDataReader))
        //    {
        //        IDataReader reader = new JmbDataReaderWrapper(database.ExecuteReader(cmd));
        //        return (T)reader;
        //    }
        //    else
        //    {
        //        var ret = (T)database.ExecuteScalar(cmd);
        //        return ret;
        //    }
        //}

        //}
        //}
    }
}