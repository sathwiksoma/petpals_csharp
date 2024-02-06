using PetPals.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Repository
{
    internal interface IPetShelterRepository
    {
        bool AddPet(Pet pet);
        bool RemovePet(int id);
        List<Pet> GetAllPets();
    }
}
