using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        SqlConnection Connection { get; }

        List<Owner> GetAllOwners();

        Owner GetOwnerById(int id);
    }
}