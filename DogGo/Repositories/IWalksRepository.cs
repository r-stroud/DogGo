using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public interface IWalksRepository
    {
        SqlConnection Connection { get; }

        List<Walk> GetWalksByWalkerId(int walkerId);
    }
}