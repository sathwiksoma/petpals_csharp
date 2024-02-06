using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Service
{
    internal interface IAdoptionService
    {
        void NewAdoptionEvent();
        void ViewAllEvents();
        void RegisterParticipantForEvent();
        void ViewAllParticipants();
        void AdoptPet();
    }
}
