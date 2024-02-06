using PetPals.Exceptions;
using PetPals.Model;
using PetPals.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Service
{
    internal class DonationService : IDonationService
    {
        public void CashDonation()
        {
            Console.WriteLine("\t\t\t\t Cash Donation \n");
            Console.Write("\nPlease enter your name: ");
            CashDonationRepository cashDonation = new CashDonationRepository();
            cashDonation.DonorName = Console.ReadLine();
            Console.Write("\nPlease enter the amount of cash min of 500 rupees ");
            try
            {
                cashDonation.Amount = decimal.Parse(Console.ReadLine());
                if (cashDonation.RecordDonation())
                {
                    Console.WriteLine($"Thanks for the donation {cashDonation.Amount}");
                    Thread.Sleep(2000);
                }
                else
                {
                    throw new InsufficientFundsException("Donation amount is less than 500 rupees ");
                }

            } 
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
            }
        }

        public void ItemDonation()
        {
            Console.WriteLine("\t\t\t\tCash Donation\n");
            Console.Write("\nPlease enter your name: ");
            ItemDonationRepository itemDonation = new ItemDonationRepository();
            itemDonation.DonorName = Console.ReadLine();
            Console.Write("\nPlease enter the name of the item you would like to donate: ");
            itemDonation.ItemType = Console.ReadLine();
            if (itemDonation.RecordDonation())
            {
                Console.WriteLine($"Thanks for the donation {itemDonation.ItemType}");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Donation not successfull");
                Thread.Sleep(2000);
            }
        }
    }
}
