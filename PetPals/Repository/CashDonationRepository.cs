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
    internal class CashDonationRepository : Donation
    {
        public string connectionString;
        SqlCommand cmd = null;
        public CashDonationRepository() {
            connectionString = DBConnUtil.GetConnectedString();
            cmd = new SqlCommand();
        }
        public decimal Amount { get; set; }

        public override bool RecordDonation()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))

                {
                    cmd.CommandText = "insert into Donations  (DonorName, DonationType, DonationAmount, DonationDate,ShelterID)  values(@name, @type, @cash, @date,@id)";
                    cmd.Parameters.AddWithValue("@name", DonorName);
                    string donationType = "Monetary";
                    cmd.Parameters.AddWithValue("@type", donationType);
                    cmd.Parameters.AddWithValue("@cash", Amount);
                    Date = DateTime.Now;
                    cmd.Parameters.AddWithValue("@date", Date);
                    int shelter = new Random().Next(1, 6);
                    cmd.Parameters.AddWithValue("@id", shelter);

                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
