using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public interface INeighborhoodRepository
    {
        SqlConnection Connection { get; }

        List<Neighborhood> GetAll();
    }
}