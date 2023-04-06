using Microsoft.Data.SqlClient;

namespace DogGo.Utils
{
    public class DBUtils
    {
        public static bool IsDbNull(SqlDataReader reader, string column)
        {
            return reader.IsDBNull(reader.GetOrdinal(column));
        }
    }
}
