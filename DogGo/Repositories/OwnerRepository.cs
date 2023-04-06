using DogGo.Models;
using DogGo.Utils;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace DogGo.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;

        public OwnerRepository(IConfiguration config)
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

        public List<Owner> GetAllOwners()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT O.Id, O.Email, O.[Name], O.[Address], O.Phone,
D.Id AS DogId, D.[Name] AS DogName, D.Breed, D.Notes, D.ImageUrl,
N.[Name] AS NeighborhoodName
FROM Owner O
JOIN Dog D
ON D.OwnerId = O.Id
JOIN Neighborhood N
ON N.Id = O.NeighborhoodId
";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Owner> owners = new List<Owner>();
            
                    while (reader.Read())
                    {

                        int ownerId = reader.GetInt32(reader.GetOrdinal("Id"));
                        Owner existingOwner = owners.FirstOrDefault(x => x.Id == ownerId);
                        if(existingOwner == null)
                        {
                            existingOwner = new Owner()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NieghborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Dogs = new List<Dog>()
                            };

                            owners.Add(existingOwner);
                        }
                        
                      

                        if( reader.GetInt32(reader.GetOrdinal("DogId")) != null)
                        {

                            string notes;
                            string image;

                            if (DBUtils.IsDbNull(reader, "Notes"))
                            {
                                notes = "none";
                            }
                            else
                            {
                                notes = reader.GetString(reader.GetOrdinal("Notes"));
                            }

                            if (DBUtils.IsDbNull(reader, "ImageUrl"))
                            {
                                image = "none";
                            }
                            else
                            {
                                image = reader.GetString(reader.GetOrdinal("ImageUrl"));
                            }


                            Dog dog = new Dog()
                            {
                                
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Notes = notes,
                                ImageUrl = image
                            };

                            existingOwner.Dogs.Add(dog);
                        }

                        
                    }

                    reader.Close();

                    return owners;
                }
            }
        }

        public Owner GetOwnerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT O.Id, O.Email, O.[Name], O.[Address], O.Phone,
D.Id AS DogId, D.[Name] AS DogName, D.Breed, D.Notes, D.ImageUrl,
N.[Name] AS NeighborhoodName
FROM Owner O
JOIN Dog D
ON D.OwnerId = O.Id
JOIN Neighborhood N
ON N.Id = O.NeighborhoodId
WHERE Id = @id
";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if(reader.Read())
                    {
                        Owner owner = new Owner()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            NieghborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone"))

                        };

                        reader.Close();
                        return owner;

                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                   
                }
            }
        }

    }
}
