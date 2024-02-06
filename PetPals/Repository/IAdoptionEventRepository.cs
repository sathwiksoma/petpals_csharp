using PetPals.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Repository
{
    internal interface IAdoptionEventRepository
    {
        bool Adopt(int petId, int userId);
        List<AdoptionEvent> ShowAllEvents();
        bool HostEvent(AdoptionEvent adoptionEvent);
        bool RegisterParticipant(Participants participant);
        List<Participants> GetAllParticipants();
    }
}
