using PetPals.Model;
using PetPals.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Repository
{
    internal class PetShelterRepository : IPetShelterRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public PetShelterRepository()
        {
            connectionString = DBConnUtil.GetConnectedString();
            cmd = new SqlCommand();
        }

        public bool AddPet(Pet pet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString)) 
                {
                    cmd.CommandText = "insert into Pets values (@name, @age, @breed, @type, @petAvailability, @id)";
                    cmd.Parameters.AddWithValue("@name", pet.Name);
                    cmd.Parameters.AddWithValue("@age", pet.Age);
                    cmd.Parameters.AddWithValue("@breed", pet.Breed);
                    cmd.Parameters.AddWithValue("@type", pet.Type);
                    bool petAvailability = true;
                    cmd.Parameters.AddWithValue("@petAvailability", petAvailability);
                    int shelter = new Random().Next(1, 6);
                    cmd.Parameters.AddWithValue("@id", shelter);
                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public List<Pet> GetAllPets()
        {
            List<Pet> availablePets = new List<Pet>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Pets where AvailableForAdoption=1";
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Pet pet = new Pet((string)reader["Name"], (int)reader["age"], (string)reader["Breed"]);
                        pet.Type = (string)reader["Type"];
                        availablePets.Add(pet);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return availablePets;
        }

        public bool RemovePet(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "delete from Pets where PetID=@petid";
                    cmd.Parameters.AddWithValue("@petid", id);
                    cmd.Connection = conn;
                    conn.Open();
                    int removePetStatus = cmd.ExecuteNonQuery();
                    return removePetStatus > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
