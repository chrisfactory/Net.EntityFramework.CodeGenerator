using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace DataBaseAccess
{
    public class DBAccessBase
    {
        private readonly string _cnx;
        public DBAccessBase(string cnx)
        {
            _cnx = cnx;
        }
        protected async Task<SqlDataReader> ExecuteReaderAsync(string storedProcedureName, IReadOnlyDictionary<string, object?> parameters, int? timeout = null)
        {
            var conn = new SqlConnection(_cnx);
            await conn.OpenAsync();

            await using (var cmd = new SqlCommand())
            {
                if (parameters != null)
                    foreach (var parameter in parameters)
                    {
                        var pValue = parameter.Value;
                        if (pValue == null)
                            pValue = DBNull.Value; 
                        cmd.Parameters.Add(new SqlParameter(parameter.Key, pValue));
                    }

                cmd.Connection = conn;
                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }
    }
}