using PetPals.Exceptions;
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
    internal class AdoptionEventRepository : IAdoptionEventRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public AdoptionEventRepository()
        {
            connectionString = DBConnUtil.GetConnectedString();
            cmd = new SqlCommand();
        }
        public bool Adopt(int petId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "update Pets set AvailableForAdoption=0 where PetID=@petid";
                    cmd.Parameters.AddWithValue("@petid", petId);
                    cmd.Connection = conn;
                    conn.Open();
                    int petUpdate = cmd.ExecuteNonQuery();
                    if (petUpdate <= 0)
                    {
                        throw new AdoptionException("The pet is already adopted");
                    }
                    cmd.CommandText = "insert into Adoption values(@petid, @userid,@date);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@petid", petId);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    int userUpdate = cmd.ExecuteNonQuery();
                    return userUpdate > 0 && petUpdate > 0;
                }
            }catch(AdoptionException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public List<Participants> GetAllParticipants()
        {
            List<Participants> allParticipants = new List<Participants>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Participants";
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Participants participant = new Participants();
                        participant.ParticipantID = (int)reader["ParticipantID"];
                        participant.ParticipantName = (string)reader["ParticipantName"];
                        participant.ParticipantType = (string)reader["ParticipantType"];
                        participant.EventId = (int)reader["EventID"];
                        allParticipants.Add(participant);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return allParticipants;
        }

        public bool HostEvent(AdoptionEvent adoptionEvent)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "insert into AdoptionEvents values (@name2, @date2, @location2)";
                    cmd.Parameters.AddWithValue("@name2", adoptionEvent.EventName);
                    cmd.Parameters.AddWithValue("@date2", adoptionEvent.EventDate);
                    cmd.Parameters.AddWithValue("@location2", adoptionEvent.Location);

                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }
            catch (Exception e)
            {
                Console.Write("\nReturning to previous menu...");
            }
            return false;
        }

        public bool RegisterParticipant(Participants participant)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "insert into Participants values (@name1, @type1, @id1)";
                    cmd.Parameters.AddWithValue("@name1", participant.ParticipantName);
                    cmd.Parameters.AddWithValue("@type1", participant.ParticipantType);
                    cmd.Parameters.AddWithValue("@id1", participant.EventId);

                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public List<AdoptionEvent> ShowAllEvents()
        {
            List<AdoptionEvent> allEvents = new List<AdoptionEvent>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from AdoptionEvents";
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AdoptionEvent adoptionEvent = new AdoptionEvent();
                        adoptionEvent.EventID = (int)reader["EventID"];
                        adoptionEvent.EventName = (string)reader["EventName"];
                        adoptionEvent.EventDate = (DateTime)reader["EventDate"];
                        adoptionEvent.Location = (string)reader["Location"];
                        allEvents.Add(adoptionEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return allEvents;
        }
    }
}
