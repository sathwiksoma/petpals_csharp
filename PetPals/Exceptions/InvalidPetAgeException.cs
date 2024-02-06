using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Exceptions
{
    internal class InvalidPetAgeException : Exception
    {
        public InvalidPetAgeException() { }
        public InvalidPetAgeException(string message) : base(message) { }
    }
}
