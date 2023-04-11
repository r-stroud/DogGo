using Microsoft.Data.SqlClient;
using DogGo.Models;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;
        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT DISTINCT
WLKS.[Date],
O.[Name],
WLKS.Duration
FROM Walker W
JOIN Walks WLKS
ON WLKS.WalkerId = W.Id
LEFT JOIN Dog D
ON WLKS.DogId = D.Id
LEFT JOIN Owner O
ON O.Id = D.OwnerId
WHERE W.Id= @id
";
                    cmd.Parameters.AddWithValue("@id", walkerId);
                    List<Walk> walks = new List<Walk>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Dog = new Dog()
                            {
                                Owner = new Owner()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                }
                            },
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration"))
                        };
                        walks.Add(walk);
                    }

                    reader.Close();
                    return walks;

                }
            }
        }
    }
}
