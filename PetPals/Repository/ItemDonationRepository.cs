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
    internal class ItemDonationRepository : Donation
    {
        public string connectionString;
        SqlCommand cmd = null;
        public ItemDonationRepository()
        {
            connectionString = DBConnUtil.GetConnectedString();
            cmd = new SqlCommand();
        }
        public string ItemType { get; set; }

        public override bool RecordDonation()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))

                {
                    cmd.CommandText = "insert into Donations  (DonorName, DonationType, DonationItem, DonationDate,ShelterID)  values(@name, @type, @item, @date,@id)";
                    cmd.Parameters.AddWithValue("@name", DonorName);
                    string donationType = "In-Kind";
                    cmd.Parameters.AddWithValue("@type", donationType);
                    cmd.Parameters.AddWithValue("@item", donationType);
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
