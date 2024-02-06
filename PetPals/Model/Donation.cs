using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Model
{
    internal abstract class Donation
    {
        public string DonorName { get; set; }
        public decimal Amount { get; set; }

        

        public DateTime DonationDate;
        public DateTime Date
        {
            get { return DonationDate; }
            set { DonationDate = DateTime.Now; }
        }

        public abstract bool RecordDonation();

        public Donation() { }

        public Donation(string donorName)
        {
            DonorName = donorName;
        }
    }
}
